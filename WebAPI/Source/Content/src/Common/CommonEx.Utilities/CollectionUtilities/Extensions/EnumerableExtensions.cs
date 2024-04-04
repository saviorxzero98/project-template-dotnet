using System.Threading.Tasks.Dataflow;

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
    }
}
