using QueueWhisArray.Model;
using System.Collections;
using QueueWhisArray.Enums;


namespace QueueWhisArray.Queue
{
    sealed class QueueArray<T> : IEnumerable<T>
    {
        private T[] queue;
        public int Count { get; private set; }
        private int MaxCount;
        private int Position { get; set; } = 0;
        public bool IsEmpty { get => Count == 0; }
        public QueueArray(int maxCount = 100)
        {
            MaxCount = maxCount;
            queue = new T[MaxCount];
        }

        public void Enqueue(T item)
        {
            if(IsEmpty)
            {
                queue[0] = item;
                Count++;
                return;
            }
            queue[Count] = item;
            Count++;
        }

        public Result<T> Dequeue()
        {
            var resultDequeue = new Result<T> { IsSuccess = false } ;
            if (IsEmpty)
            {
                resultDequeue.Status = StatusError.ArgumentNull;
                resultDequeue.TextError = "Queue is empty";
                return resultDequeue;
            }
            resultDequeue.Payload = queue[Position];
            resultDequeue.IsSuccess = true;
            resultDequeue.TextError = "Dequeue completed success";
            resultDequeue.Status = StatusError.Success;
            Position++;
            return resultDequeue;
        }

        public Result<T> First
        {
            get
            {
                var resultFirst = new Result<T> { IsSuccess = false };
                if (IsEmpty)
                {
                    resultFirst.Status = StatusError.ArgumentNull;
                    resultFirst.TextError = "Queue is empty";
                    return resultFirst;
                }
                resultFirst.Payload = queue[Position];
                resultFirst.IsSuccess = true;
                resultFirst.TextError = "Get first completed success";
                resultFirst.Status = StatusError.Success;
                return resultFirst;
            }
        }
        public Result<T> Last
        {
            get
            {
                var resultLast = new Result<T> { IsSuccess = false };
                if (IsEmpty)
                {
                    resultLast.Status = StatusError.ArgumentNull;
                    resultLast.TextError = "Queue is empty";
                    return resultLast;
                }
                resultLast.Payload = queue[Count-1];
                resultLast.IsSuccess = true;
                resultLast.TextError = "Get first completed success";
                resultLast.Status = StatusError.Success;
                return resultLast;
            }
        }

        public void Clear()
        {
            if (IsEmpty)
                return;

            queue = new T[MaxCount];
            Count = 0;
            Position = 0;
        }

        public Result<bool> Contains(T item)
        {
            var resultContains = new Result<bool>() { IsSuccess = false };
            if (IsEmpty)
            {
                resultContains.Status = StatusError.ArgumentNull;
                resultContains.TextError = "Queue is empty";
                return resultContains;
            }
            foreach(var ii in queue)
            {
                if(ii.Equals(item))
                {
                    resultContains.Payload = true;
                    resultContains.IsSuccess = true;
                    resultContains.TextError = "Get first completed success";
                    resultContains.Status = StatusError.Success;
                    return resultContains;
                }
            }
            return resultContains;
        }

        public IEnumerator<T> GetEnumerator()
        {
            
            for(int i = Position; i < Count; i++)
            {
                yield return queue[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
