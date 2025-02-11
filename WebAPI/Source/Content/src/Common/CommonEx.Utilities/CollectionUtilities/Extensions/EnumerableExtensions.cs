﻿using System.Threading.Tasks.Dataflow;

namespace CommonEx.Utilities.CollectionUtilities.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Is Null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this IEnumerable<T> value)
        {
            return (value == null);
        }

        /// <summary>
        /// Is Empty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this IEnumerable<T> value)
        {
            return (!value.Any());
        }

        /// <summary>
        /// Is Null Or Empty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> value)
        {
            return (value == null || !value.Any());
        }

        /// <summary>
        /// Parallel For Each Async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="body"></param>
        /// <param name="maxDop"></param>
        /// <returns></returns>
        public static Task ParallelForEachAsync<T>(this IEnumerable<T> list, 
                                                   Func<T, Task> body,
                                                   int maxDop = DataflowBlockOptions.Unbounded)
        {
            var options = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = maxDop
            };

            var block = new ActionBlock<T>(body, options);

            foreach (var item in list)
            {
                block.Post(item);
            }

            block.Complete();
            return block.Completion;
        }

        /// <summary>
        /// Parallel For Each Async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="body"></param>
        /// <param name="scheduler"></param>
        /// <param name="maxDop"></param>
        /// <returns></returns>
        public static Task ParallelForEachAsync<T>(this IEnumerable<T> list,
                                                   Func<T, Task> body,
                                                   TaskScheduler scheduler,
                                                   int maxDop = DataflowBlockOptions.Unbounded)
        {
            var options = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = maxDop
            };
            if (scheduler != null)
            {
                options.TaskScheduler = scheduler;
            }

            var block = new ActionBlock<T>(body, options);

            foreach (var item in list)
            {
                block.Post(item);
            }

            block.Complete();
            return block.Completion;
        }

        /// <summary>
        /// Chunk By
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="chunkLength"></param>
        /// <returns></returns>
        public static List<List<T>> ChunkBy<T>(this IEnumerable<T> list, int chunkLength)
        {
            return list.Select((x, i) => new { Index = i, Value = x })
                       .GroupBy(x => x.Index / chunkLength)
                       .Select(x => x.Select(v => v.Value).ToList())
                       .ToList();
        }

        /// <summary>
        /// Spilt
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="parts"></param>
        /// <returns></returns>
        public static List<List<T>> Spilt<T>(this IEnumerable<T> list, int parts)
        {
            return list.Select((x, i) => new { Index = i, Value = x })
                       .GroupBy(x => (x.Index + 1) / (list.Count() / parts) + 1)
                       .Select(x => x.Select(y => y.Value).ToList())
                       .ToList();
        }

        /// <summary>
        /// 條件式 Where
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="condition"></param>
        /// <param name="trueCondiction"></param>
        /// <param name="falseCondiction"></param>
        /// <returns></returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> value,
                                                bool condition,
                                                Func<IEnumerable<T>, IEnumerable<T>> trueCondiction,
                                                Func<IEnumerable<T>, IEnumerable<T>> falseCondiction = null)
        {
            if (condition)
            {
                if (trueCondiction != null)
                {
                    return trueCondiction.Invoke(value);
                }
            }
            else
            {
                if (falseCondiction != null)
                {
                    return falseCondiction.Invoke(value);
                }
            }
            return value;
        }

        /// <summary>
        /// Find Index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int FindIndex<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            var index = list.Select((value, index) => new { value, index })
                            .Where(x => predicate(x.value))
                            .Select(x => x.index)
                            .FirstOrDefault();
            return index;
        }

        /// <summary>
        /// 滿足條件時，將 Value 加入到 List 中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void AddIf<TValue>(this IList<TValue> list, Func<TValue, bool> predicate, TValue value)
        {
            if (predicate(value))
            {
                list.Add(value);
            }
        }

        /// <summary>
        /// Add if not exists
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AddIfNotExists<TValue>(this List<TValue> list, TValue value)
        {
            var isNotExists = !list.Contains(value);
            if (!list.Contains(value))
            {
                list.Add(value);
            }
            return isNotExists;
        }
        /// <summary>
        /// Add if not exists
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AddIfNotExists<TValue>(this List<TValue> list, Predicate<TValue> predicate, TValue value)
        {
            var isNotExists = !list.Exists(predicate);
            if (isNotExists)
            {
                list.Add(value);
            }
            return isNotExists;
        }

        /// <summary>
        /// Remove if not exists
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool RemoveIfExists<TValue>(this List<TValue> list, TValue value)
        {
            var isExist = list.Contains(value);
            if (isExist)
            {
                list.Remove(value);
            }
            return isExist;
        }
        /// <summary>
        /// Remove if not exists
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool RemoveIfExists<TValue>(this List<TValue> list, Predicate<TValue> predicate, TValue value)
        {
            var isExists = list.Exists(predicate);
            if (isExists)
            {
                list.Remove(value);
            }
            return isExists;
        }

        /// <summary>
        /// 兩個陣列取差集
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="listA"></param>
        /// <param name="listB"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TValue> ExceptBy<TValue>(this IEnumerable<TValue> listA, IEnumerable<TValue> listB,
                                                           Func<TValue, TValue, bool> predicate)
        {
            var newList = listA.Where(a => !listB.Any((b) => predicate(a, b)));
            return newList;
        }
    }
}
