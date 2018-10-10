using System;
using System.Collections.Generic;
using System.Linq;
using static Instruments.Data;

namespace Instruments
{
    public static class Handler
    {
        #region DescreteFunctions

        public static float GetDiscreteRowInitialMoment(byte[] row, int momentOrder)
        {
            float collectionSize = row.Length;
            float numerator = 0F;

            for (int i = 0; i < collectionSize; i++)
            {
                numerator += (float) Math.Pow(row[i], momentOrder);
            }

            return numerator / collectionSize;
        }

        public static float GetDiscreteRowCentralMoment(float[,] table, byte[] row, int momentOrder)
        {
            float collectionSize = row.Length;
        
            float numerator = 0F;
            float expectedValue = GetDiscreteRowExpectedValue(row);
           
            for (int i = 0; i < row.Length; i++)
            {
                numerator += (float)Math.Pow(row[i] - expectedValue, momentOrder);

            }
            return numerator / collectionSize;
        }


        public static float GetDiscreteRowDispersion(float[,] table, byte[] row)
        {
            return GetDiscreteRowCentralMoment(table, row, 2);
        }

        public static float GetDiscreteRowAverageSquareDeviation(float[,] table, byte[] row)
        {
            return (float) Math.Sqrt(GetDiscreteRowDispersion(table, row));
        }

        public static float GetDiscreteRowAsymmetry(float[,] table, byte[] row)
        {
            float averageSquareDeviation = (float) Math.Pow(GetDiscreteRowAverageSquareDeviation(table, row), 3);
            return GetDiscreteRowCentralMoment(table, row, 3) / averageSquareDeviation;
        }

        public static float GetDiscreteRowExcess(float[,] table, byte[] row)
        {
            float averageSquareDeviation = (float) Math.Pow(GetDiscreteRowAverageSquareDeviation(table, row), 4);
            return GetDiscreteRowCentralMoment(table, row, 4) / averageSquareDeviation - 3;
        }

        public static float GetDiscreteRowVariationCoefficient(float[,] table, byte[] row)
        {
            return GetDiscreteRowAverageSquareDeviation(table, row) / GetDiscreteRowExpectedValue(row) * 100;
        }

        public static float[] GetDiscreteRowModes(float[,] table)
        {
            var rows = IdDescreteRow.Distinct().ToArray().Length;
            var mode = table[1, 0];

            for (int i = 0; i < rows; i++)
            {
                if (mode < table[1, i])
                {
                    mode = table[1, i];
                }
            }

            List<float> modes = new List<float>();

            for (int i = 0; i < rows; i++)
            {
                if (Equals(mode, table[1, i]))
                {
                    modes.Add(table[0, i]);
                }
            }

            return modes.ToArray();
        }

        public static float GetDiscreteRowMedian(float[,] table)
        {
            float median;

            int collectionValue = (int) table[3, table.GetLength(1) - 1];


            if (collectionValue % 2 == 0)
            {
                int leftValue = collectionValue / 2;
                int rightValue = leftValue + 1;

                int i = 0;
                while (table[3, i] < leftValue)
                {
                    i++;
                }

                int leftId = (int) table[0, i];

                if (table[3, i] < rightValue)
                {
                    i++;
                }

                int rightId = (int) table[0, i];

                median = (leftId + rightId) / 2.0F;
            }
            else
            {
                int value = (int) Math.Ceiling((double) collectionValue / 2);

                int i = 0;
                while (table[3, i] < value)
                {
                    i++;
                }

                median = table[0, i];
            }

            return median;
        }

        public static float GetDiscreteRowExpectedValue(byte[] row)
        {
            return GetDiscreteRowInitialMoment(row, 1);
        }

        public static float[,] GetDiscreteTable(byte[] discreteRow)
        {
            byte[] values = discreteRow.Distinct().ToArray();
            float[,] table = new float[4, values.Length];
            int collectionVolume = discreteRow.Length;
            int cumulativeFrequency = 0;

            for (int i = 0; i < values.Length; i++)
            {
                table[0, i] = values[i];

                int count = 0;
                for (int j = 0; j < collectionVolume; j++)
                {
                    if (discreteRow[j] == values[i])
                    {
                        count++;
                    }
                }

                table[1, i] = count;
                table[2, i] = table[1, i] / collectionVolume;
                table[3, i] = cumulativeFrequency += (int)table[1, i];
            }

            return table;
        }

        #endregion

        #region VariationalFunctions

        public static float GetIntervalRowVariationalRange(int[] intervalRow)
        {
            return intervalRow.Max() - intervalRow.Min();
        }

        public static int GetIntervalsNumber(int[] intervalRow)
        {
            return (int)Math.Round(1 + 3.222 * Math.Log10(intervalRow.Length));
        }

        public static int GetIntervalsStep(int[] intervalRow)
        {
            return (int) Math.Ceiling(GetIntervalRowVariationalRange(intervalRow) / GetIntervalsNumber(intervalRow));
        }

        public static float[,] GetIntervalTable(int[] intervalRow, int intervalsStep, int intervalsNumber)
        {
            int collectionVolume = intervalRow.Length;

            float[,] table = new float[6, intervalsNumber];
            int cumulativeFrequency = 0;
            int leftIntervalsBorder = intervalRow[0];
            

            for (int i = 0; i < table.GetLength(1); i++)
            {
                table[0, i] = leftIntervalsBorder;
                table[1, i] = leftIntervalsBorder += intervalsStep;
                int count = 0;
                for (int j = 0; j < collectionVolume; j++)
                {
                    if (intervalRow[j] >= table[0, i] && intervalRow[j] < table[1, i])
                    {
                        count++;
                    }
                }
                table[2, i] = count;
                table[3, i] = table[2, i] / collectionVolume;
                table[4, i] = cumulativeFrequency += (int) table[2, i];
                table[5, i] = (table[0, i] + table[1, i]) / 2;
            }
            
            return table;
        }

        public static float GetIntervalRowExpectedValue(float[,] table, int[] row)
        {
            return GetIntervalRowInitialMoment(table, row, 1);
        }

        public static float GetIntervalRowTotalDispersion(float[,] table, int[] row)
        {
            return GetIntervalRowCentralMoment(table, row, 2);
        }

        public static float GetIntervalRowInitialMoment(float[,] table, int[] row, int order)
        {
            float moment = 0f;
            int collectionSize = row.Length;

            for (int i = 0; i < row.Length; i++)
            {
                moment += (float) Math.Pow(row[i], order);
            }

            return moment / collectionSize;
        }

        public static float GetIntervalRowCentralMoment(float[,] table, int[] row, int momentOrder)
        {

            float collectionSize = row.Length;
            float numerator = 0F;
            float expectedValue = GetIntervalRowExpectedValue(table, row);

            for (int i = 0; i < row.Length; i++)
            {
                numerator += (float)Math.Pow(row[i] - expectedValue, momentOrder);
            }

            return numerator / collectionSize;
        }

        public static float GetInteravalRowIntergroupDispersion(float[,] table, int[] intervalRow)
        {
            float collectionSize = intervalRow.Length;
            float numerator = 0;
            float totalExpectedValue = GetIntervalRowExpectedValue(table, intervalRow);

            for (int i = 0; i < table.GetLength(1); i++)
            {
                float groupVolume = table[2, i];
                int[] currentGroup = intervalRow.Where(value => value >= table[0, i] && value < table[1, i]).ToArray();

                float currentExpectedValue = GetIntervalRowExpectedValue(table, currentGroup);
                numerator += (float)(Math.Pow(currentExpectedValue - totalExpectedValue, 2) * groupVolume);
            }

            return numerator / collectionSize;
        }

        public static float GetIntervalRowAverageGroupDispersion(float[,] table, int[] intervalRow)
        {
            List<float> groupDispersions = new List<float>();
            int collectionSize = intervalRow.Length;

            for (int i = 0; i < table.GetLength(1); i++)
            {
                int[] currentGroup = intervalRow.Where(value => (value >= table[0, i] && value < table[1, i])).ToArray();
                float currentDispersion = GetIntervalRowTotalDispersion(table, currentGroup) * table[2, i];

                groupDispersions.Add(currentDispersion);
            }

            return groupDispersions.Sum() / collectionSize;
        }

        public static float GetIntervalRowAverageSquareDeviation(float[,] table, int[] row)
        {
            return (float) Math.Sqrt(GetIntervalRowTotalDispersion(table, row));
        }

        public static float GetIntervalRowAsymmetry(float[,] table, int[] row)
        {
            float averageSquareDeviation = (float) Math.Pow(GetIntervalRowAverageSquareDeviation(table, row), 3);
            return GetIntervalRowCentralMoment(table, row, 3) / averageSquareDeviation;
        }

        public static float GetIntervalRowExcess(float[,] table, int[] row)
        {
            float averageSquareDeviation = (float) Math.Pow(GetIntervalRowAverageSquareDeviation(table, row), 4);
            return GetIntervalRowCentralMoment(table, row, 4) / averageSquareDeviation - 3;
        }

        public static float GetIntervalRowVariationCoefficient(float[,] table, int[] row)
        {
            return GetIntervalRowAverageSquareDeviation(table, row) / GetIntervalRowExpectedValue(table, row) * 100;
        }

        public static float[] GetIntervalRowModes(float[,] table)
        {
            float maxFrequency = table[2, 0];

            for (int i = 0; i < table.GetLength(1); i++)
            {
                if (table[2, i] > maxFrequency)
                {
                    maxFrequency = table[2, i];
                }
            }

            List<float> modes = new List<float>();

            for (int i = 0; i < table.GetLength(1); i++)
            {
                if (Equals(table[2, i], maxFrequency))
                {
                    float lowerIntervalsBorder = table[0, i];
                    float intervalValue = table[1, i] - table[0, i];

                    int frequencyBeforeInterval = 0;
                    if (i != 0)
                    {
                        frequencyBeforeInterval = (int)table[2, i - 1];
                    }
                    
                    int frequencyAfterInterval = (int) table[2, i + 1];

                    float mode = lowerIntervalsBorder + intervalValue *
                                 ((maxFrequency - frequencyBeforeInterval) /
                                  ((maxFrequency - frequencyBeforeInterval) + (maxFrequency - frequencyAfterInterval)));

                    modes.Add(mode);
                }
            }

            return modes.ToArray();
        }

        public static float GetIntervalRowMedian(float[,] table)
        {
            float median = 0;

            int collectionValue = (int) table[4, table.GetLength(1) - 1];
            int halfOfCollectionValue = (int) Math.Ceiling((double) table[4, table.GetLength(1) - 1] / 2);

            if (collectionValue % 2 == 0)
            {
                int leftValue = collectionValue / 2;
                int rightValue = leftValue + 1;

                int i = 0;
                while (table[4, i] < leftValue)
                {
                    i++;
                }

                int lowerIntervalsBorder = (int) table[0, i];
                int intervalValue = (int) (table[1, i] - table[0, i]);
                int collectionValueBeforeInterval = 0;
                if (i != 0)
                {
                    collectionValueBeforeInterval = (int) table[4, i - 1];
                }

                int intrevalFrequency = (int) table[2, i];
                float leftId = lowerIntervalsBorder + intervalValue *
                               ((halfOfCollectionValue - collectionValueBeforeInterval) / intrevalFrequency);

                if (table[4, i] < rightValue)
                {
                    i++;
                }

                lowerIntervalsBorder = (int) table[0, i];
                collectionValueBeforeInterval = 0;
                intervalValue = (int) (table[1, i] - table[0, i]);
                if (i != 0)
                {
                    collectionValueBeforeInterval = (int) table[4, i - 1];
                }

                intrevalFrequency = (int) table[2, i];
                float rightId = lowerIntervalsBorder + intervalValue *
                                ((halfOfCollectionValue - collectionValueBeforeInterval) / intrevalFrequency);

                median = (leftId + rightId) / 2;
            }
            else
            {
                for (int i = 0; i < table.GetLength(1); i++)
                {
                    if (halfOfCollectionValue < table[4, i])
                    {
                        float lowerIntervalsBorder = table[0, i];
                        float intervalValue = table[1, i] - table[0, i];
                        int collectionValueBeforeInterval = 0;
                        if (i != 0)
                        {
                            collectionValueBeforeInterval = (int) table[4, i - 1];
                        }


                        float intrevalFrequency = table[2, i];

                        median = lowerIntervalsBorder + intervalValue *
                                 ((halfOfCollectionValue - collectionValueBeforeInterval) / intrevalFrequency);

                        break;
                    }
                }
            }

            return median;
        }

        #endregion
    }
}
