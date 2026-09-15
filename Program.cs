using FunctionalDataStructures;

var historyQueue = new StateHistoryQueue<int>();
historyQueue.Enqueue(10);
historyQueue.Enqueue(20);
historyQueue.Enqueue(30);
historyQueue.Undo();
historyQueue.Redo();
historyQueue.Dequeue();
historyQueue.Undo();