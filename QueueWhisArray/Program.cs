using QueueWhisArray.Queue;

var _queue = new QueueArray<int>();

_queue.Enqueue(1);
_queue.Enqueue(2);
_queue.Enqueue(3);
_queue.Enqueue(4);

foreach(var ii in _queue)
    Console.WriteLine(ii);