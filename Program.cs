using FunctionalDataStructures;

var q1 = FunctionalDataStructures.Queue<int>.Empty;
var q2 = q1.Enqueue(10);
var q3 = q2.Enqueue(20);
var q4 = q3.Enqueue(30);
var q5 = q4.Dequeue();
var q6 = q5.Dequeue();
var q7 = q6.Dequeue();
Console.WriteLine(q1.Bracket());
Console.WriteLine(q2.Bracket());
Console.WriteLine(q3.Bracket());
Console.WriteLine(q4.Bracket());
Console.WriteLine(q5.Bracket());
Console.WriteLine(q6.Bracket());
Console.WriteLine(q7.Bracket());