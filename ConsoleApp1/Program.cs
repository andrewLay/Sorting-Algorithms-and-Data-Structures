/*
SORTING ALGORITHMS AND DATA STRUCTURES CONSOLE APPLICATION
==========================================================
Experimenting with the runtime of various algorithms implemented as functions.
Details include time complexity (in ticks = 1 sec/Freq) and best cases for algorithm usage.

1. Selection Sort   (18,000')
2. Insertion Sort   (70')
3. Bubble Sort      (38,000')
4. Merge Sort       (8,000')
5. Quick Sort       (16,000')
6. Heap Sort ?      (')
...
7. Dictionary
8. Hash Map ?
9. Linked List ?

Unsorted array supplied as text file called 'InputArray.txt' with 1,000 unsorted numbers.
 */

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        const string dirInputArray = @"C:\Users\Andrew Lay\source\repos\Sorting Algorithms and Data Structures\InputArray.txt";

        static void Main(string[] args)
        {
            bool isTerminate = false;
            string userInput = null;
            float[] arrayForSorting = Array.ConvertAll(File.ReadAllLines(dirInputArray), float.Parse);
            float[] sortedArray;
            List<float> listForSorting = Enumerable.ToList(arrayForSorting);
            List<float> sortedList = new List<float>();
            Stopwatch stopwatch = new Stopwatch();

            while (!isTerminate)
            {
                switch (userInput)
                {
                    case "0":
                        isTerminate = true;
                        userInput = null;
                        break;
                    case "1":
                        stopwatch.Start();
                        sortedArray = SelectionSort(arrayForSorting);
                        stopwatch.Stop();
                        foreach (float line in sortedArray)
                            Console.WriteLine(line);
                        Console.WriteLine("\nAlgorithm execution time is: " + stopwatch.ElapsedTicks + " ticks");
                        userInput = null;
                        break;
                    case "2":
                        stopwatch.Start();
                        sortedArray = InsertionSort(arrayForSorting);
                        stopwatch.Stop();
                        foreach (float line in sortedArray)
                            Console.WriteLine(line);
                        Console.WriteLine("\nAlgorithm execution time is: " + stopwatch.ElapsedTicks + " ticks");
                        userInput = null;
                        break;
                    case "3":
                        stopwatch.Start();
                        sortedArray = BubbleSort(arrayForSorting);
                        stopwatch.Stop();
                        foreach (float line in sortedArray)
                            Console.WriteLine(line);
                        Console.WriteLine("\nAlgorithm execution time is: " + stopwatch.ElapsedTicks + " ticks");
                        userInput = null;
                        break;
                    case "4":
                        stopwatch.Start();
                        sortedList = MergeSort(listForSorting);
                        stopwatch.Stop();
                        foreach (float line in sortedList)
                            Console.WriteLine(line);
                        Console.WriteLine("\nAlgorithm execution time is: " + stopwatch.ElapsedTicks + " ticks");
                        userInput = null;
                        break;
                    case "5":
                        stopwatch.Start();
                        sortedArray = QuickSort(arrayForSorting, 0, arrayForSorting.Length);
                        stopwatch.Stop();
                        foreach (float line in sortedArray)
                            Console.WriteLine(line);
                        Console.WriteLine("\nAlgorithm execution time is: " + stopwatch.ElapsedTicks + " ticks");
                        userInput = null;
                        break;
                    default:
                        Console.WriteLine("\n-------------------------------------------------------------------------------------------------------------\n");
                        Console.Write("1.Selection Sort \n\n  Pick what Algorithm to Run by Entering Number (e.g. '2'). Enter '0' to terminate application: ");
                        stopwatch = new Stopwatch();
                        userInput = Console.ReadLine();
                        break;
                }
            }
            if (isTerminate)
                Environment.Exit(0);
        }

        // NOMENCLATURE: SELECT next smallest Val's index in unsorted portion of Array when compared to current Val in sorted portion.
        // RUNTIME:      O(n^2)
        // USAGE:        No auxiliary memory usage but slow for complex inputs. Easy to code and CAN be faster for simpler inputs.
        // TEST:         18,000 ticks
        private static float[] SelectionSort(float[] arrayForSorting)
        {
            for (int i = 0; i < arrayForSorting.Length - 1; i++)
            {
                int currMinIndex = i;
                for (int ii = (i + 1); ii < arrayForSorting.Length; ii++)
                {
                    if (arrayForSorting[ii] < arrayForSorting[currMinIndex])
                        currMinIndex = ii;
                }
                float temp = arrayForSorting[i];
                arrayForSorting[i] = arrayForSorting[currMinIndex];
                arrayForSorting[currMinIndex] = temp;
            }

            return arrayForSorting;
        }

        // NOMENCLATURE: INSERT current Val at index of those Vals to the left of Curr Ptr until Left Val is NOT < Curr Val.
        // RUNTIME:      O(n^2)
        // USAGE:        No auxiliary memory usage but slow for complex inputs. Faster than Selection Sort.
        // TEST:         70 ticks
        private static float[] InsertionSort(float[] arrayForSorting)
        {
            for (int i = 1; i < arrayForSorting.Length; i++)
            {
                int currLeftIndex = i;
                while (currLeftIndex > 0 && arrayForSorting[currLeftIndex - 1] > arrayForSorting[currLeftIndex])
                {
                    float temp = arrayForSorting[currLeftIndex - 1];
                    arrayForSorting[currLeftIndex - 1] = arrayForSorting[currLeftIndex];
                    arrayForSorting[currLeftIndex] = temp;
                    currLeftIndex--;
                }
            }

            return arrayForSorting;
        }

        // NOMENCLATURE: BUBBLE is comparison between every two consecutive indexes, swapping values where leftmost > rightmost.
        // RUNTIME:      O(n^2)
        // USAGE:        No auxiliary memory usage but very slow for complex inputs. Easy to code. Never used in practice.
        // TEST:         38,000 ticks
        private static float[] BubbleSort(float[] arrayForSorting)
        {
            for (int i = 1; i < arrayForSorting.Length; i++)
            {
                for (int ii = 0; ii < arrayForSorting.Length - 1; ii++)
                {
                    if (arrayForSorting[ii] > arrayForSorting[ii + 1])
                    {
                        float temp = arrayForSorting[ii];
                        arrayForSorting[ii] = arrayForSorting[ii + 1];
                        arrayForSorting[ii + 1] = temp;
                    }
                }
            }

            return arrayForSorting;
        }

        // NOMENCLATURE: MERGE two smaller lists recursively, but sort the smaller ones first in a divide-and-conquer manner.
        // RUNTIME:      O(nlogn)
        // USAGE:        Uses auxiliary memory but faster for complex inputs. O(nlogn) runtime for n-elements * numb of levels in Binary Tree i.e. Solve for (y) in 2^y = n.
        // TEST:         8,000 ticks
        private static List<float> MergeSort(List<float> listForSorting)
        {
            if (listForSorting.Count == 1)
                return listForSorting;

            List<float> listLeftmost = listForSorting.Take(listForSorting.Count / 2).ToList();
            List<float> listRightmost = listForSorting.Skip(listForSorting.Count / 2).ToList();

            listLeftmost = MergeSort(listLeftmost);
            listRightmost = MergeSort(listRightmost);

            return Helper_Merger(listLeftmost, listRightmost);
        }
        private static List<float> Helper_Merger(List<float> listLeftmost, List<float> listRightmost)
        {
            List<float> returnList = new List<float>();

            while (listLeftmost.Count > 0 && listRightmost.Count > 0)
            {
                if (listLeftmost[0] > listRightmost[0])
                {
                    returnList.Add(listRightmost[0]);
                    listRightmost.RemoveAt(0);
                }
                else
                {
                    returnList.Add(listLeftmost[0]);
                    listLeftmost.RemoveAt(0);
                }
            }
            while (listLeftmost.Count > 0)
            {
                returnList.Add(listLeftmost[0]);
                listLeftmost.RemoveAt(0);
            }
            while (listRightmost.Count > 0)
            {
                returnList.Add(listRightmost[0]);
                listRightmost.RemoveAt(0);
            }

            return returnList;
        }

        // NOMENCLATURE: QUICK refers to this highly efficient sorting algorithm, but speed is dependent on first pivot point chosen.
        // RUNTIME:      O(nlogn)
        // USAGE:        No auxiliary memory usage and faster for complex inputs. O(nlogn) runtime for n-elements * numb of levels in Binary Tree i.e. Solve for (y) in 2^y = n.
        // TEST:         16,000 ticks
        private static float[] QuickSort(float[] arrayForSorting, int leftIndex, int rightIndex)
        {
            if (leftIndex < rightIndex)
            {
                int partitionPoint = Helper_Partition(arrayForSorting, leftIndex, rightIndex);
                QuickSort(arrayForSorting, leftIndex, partitionPoint);
                QuickSort(arrayForSorting, partitionPoint + 1, rightIndex);
            }

            return arrayForSorting;
        }
        private static int Helper_Partition(float[] arrayForSorting, int leftIndex, int rightIndex)
        {
            float pivotVal = arrayForSorting[leftIndex];
            int leftWall = leftIndex;
            for (int i = (leftIndex + 1); i < rightIndex; i++)
            {
                if (arrayForSorting[i] < pivotVal)
                {
                    float temp = arrayForSorting[leftWall];
                    arrayForSorting[leftWall] = arrayForSorting[i];
                    arrayForSorting[i] = temp;

                    leftWall++;
                }
            }
            float newPivot = pivotVal;
            pivotVal = arrayForSorting[leftWall];
            arrayForSorting[leftWall] = newPivot;

            return leftWall;
        }
    }
}
