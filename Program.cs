using FunctionalDataStructures;

var historyQueue = new StateHistoryQueue<int>();
Console.WriteLine(historyQueue.Bracket());
historyQueue.Enqueue(10);
Console.WriteLine(historyQueue.Bracket());
historyQueue.Enqueue(20);
Console.WriteLine(historyQueue.Bracket());
historyQueue.Enqueue(30);
Console.WriteLine(historyQueue.Bracket());
historyQueue.Undo();
Console.WriteLine(historyQueue.Bracket());
historyQueue.Redo();
Console.WriteLine(historyQueue.Bracket());
historyQueue.Dequeue();
Console.WriteLine(historyQueue.Bracket());
historyQueue.Undo();
Console.WriteLine(historyQueue.Bracket());