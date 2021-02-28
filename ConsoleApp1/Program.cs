/*
SORTING ALGORITHMS AND DATA STRUCTURES CONSOLE APPLICATION
==========================================================
Experimenting with the runtime of various algorithms implemented as functions.
Details include time complexity (in ticks = 1 sec/Freq) and best cases for algorithm usage.

1. Selection Sort   (18,000')
2. Insertion Sort   (550')
3. Bubble Sort      (38,000')
4. Merge Sort       (8,000')
5. Quick Sort       (3,500')
6. Heap Sort        (3,500')
...
7. Dictionary       (30') [vs. 40' via Linear search]
8. Linked List      ?????

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
                    case "6":
                        stopwatch.Start();
                        sortedArray = HeapSort(arrayForSorting);
                        stopwatch.Stop();
                        foreach (float line in sortedArray)
                            Console.WriteLine(line);
                        Console.WriteLine("\nAlgorithm execution time is: " + stopwatch.ElapsedTicks + " ticks");
                        userInput = null;
                        break;
                    case "7":
                        sortedArray = InsertionSort(arrayForSorting);
                        float searchedForFloatVal = -1;
                        stopwatch.Start();
                        foreach (float value in sortedArray)                            // Linear Search Algorithm for sorted array where value being searched for is near the end
                        {
                            if (value == 24948544214908)
                            {
                                searchedForFloatVal = value;
                                break;
                            }
                        }
                        stopwatch.Stop();
                        Console.WriteLine("\nLinear search execution time is: " + stopwatch.ElapsedTicks + " ticks, and search value is: " + searchedForFloatVal);

                        Dictionary<int, float> sortedDict = CreateDictionary(sortedArray);
                        searchedForFloatVal = -1;
                        stopwatch.Restart();
                        if (sortedDict.ContainsKey(953))
                            searchedForFloatVal = sortedDict[953];
                        stopwatch.Stop();
                        Console.WriteLine("\nDictionary search execution time is: " + stopwatch.ElapsedTicks + " ticks, and search value is: " + searchedForFloatVal);
                        userInput = null;
                        break;
                    case "8":
                        userInput = null;
                        break;
                    default:
                        arrayForSorting = Array.ConvertAll(File.ReadAllLines(dirInputArray), float.Parse);
                        listForSorting = Enumerable.ToList(arrayForSorting);
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
        // USAGE:        No auxiliary memory usage but slow for larger inputs. Easy to code, and COULD be faster for simpler inputs.
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
        // USAGE:        No auxiliary memory usage but CAN slow for complex inputs. Generally very fast with optimized array that is somewhat already sorted.
        // TEST:         550 ticks
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
        // USAGE:        Uses auxiliary memory but faster for complex inputs (compared to INSERTION SORT for average and worst cases). O(nlogn) runtime for n-elements for (y) in 2^y = n.
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
        // USAGE:        No auxiliary memory usage and fastest for complex inputs. O(nlogn) runtime for n-elements * numb of levels in Binary Tree i.e. Solve for (y) in 2^y = n.
        // TEST:         3,500 ticks
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
            arrayForSorting[leftWall] = pivotVal;

            return leftWall;
        }

        // NOMENCLATURE: HEAP refers to the binary MAX heap structure, and iteratively finding the highest value for re-placement at the end.
        // RUNTIME:      O(nlogn)
        // USAGE:        No auxiliary memory usage and fastest for complex inputs. O(nlogn) runtime for n-elements * numb of levels in Binary Tree i.e. Solve for (y) in 2^y = n.
        // TEST:         3,500 ticks
        private static int heapSize;
        private static float[] HeapSort(float[] arrayForSorting)
        {
            Helper_BuildMaxHeap(arrayForSorting);
            for (int i = arrayForSorting.Length - 1; i >= 0; i--)
            {
                float temp = arrayForSorting[0];
                arrayForSorting[0] = arrayForSorting[i];
                arrayForSorting[i] = temp;
                heapSize--;
                Helper_Heapify(arrayForSorting, 0);
            }

            return arrayForSorting;
        }
        private static void Helper_BuildMaxHeap(float[] arrayForSorting)
        {
            heapSize = arrayForSorting.Length - 1;
            for (int index = (heapSize / 2); index >= 0; index--)
                Helper_Heapify(arrayForSorting, index);
        }
        private static void Helper_Heapify(float[] arrayForSorting, int index)
        {
            int maxValuIndex = index;
            int leftIndex = 2 * index + 1;
            int rightIndex = 2 * index + 2;

            if (leftIndex <= heapSize && arrayForSorting[leftIndex] > arrayForSorting[index])
                maxValuIndex = leftIndex;

            if (rightIndex <= heapSize && arrayForSorting[rightIndex] > arrayForSorting[maxValuIndex])
                maxValuIndex = rightIndex;

            if (maxValuIndex != index)
            {
                float temp = arrayForSorting[index];
                arrayForSorting[index] = arrayForSorting[maxValuIndex];
                arrayForSorting[maxValuIndex] = temp;
                Helper_Heapify(arrayForSorting, maxValuIndex);
            }
        }

        /*............................................................................................*/

        // NOMENCLATURE: Dictionary<Key, Value> e.g. [944, 24948544214908]
        // USAGE:        In the case of searching for a value (via linear or binary means), checking Dict.Contains(key) is faster than making comparisons on values.
        // TEST:         30 ticks (instead of 40' in Linear Search)
        private static Dictionary<int, float> CreateDictionary(float[] sortedArray)
        {
            Dictionary<int, float> sortedDict = new Dictionary<int, float>();

            for (int i = 0; i < sortedArray.Length; i++)
                sortedDict.Add(i + 1, sortedArray[i]);

            return sortedDict;
        }
    }
}
