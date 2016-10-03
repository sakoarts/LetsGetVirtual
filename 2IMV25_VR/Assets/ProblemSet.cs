using System;
using System.Linq;


public class ProblemSet
{
    public enum CharSet
    {
        A,
        B
    }
    private readonly char[] setA = new char[] { 'A', 'K', 'M', 'N', 'V', 'W', 'X', 'Y', 'Z' };
    private readonly char[] setB = new char[] { 'E', 'F', 'H', 'I', 'L', 'T' };

    protected char target;
    protected Boolean targetPresent;
    protected char[] implementedSet;
    protected char[][] arraysToFill;

    public ProblemSet(CharSet chosenSet, Boolean target, char[][] emptyArraysToFill)
    {
        this.arraysToFill = emptyArraysToFill;
        this.targetPresent = target;
        switch (chosenSet)
        {
            case CharSet.A:
                setA.CopyTo(implementedSet, 0);
                break;
            case CharSet.B:
                setB.CopyTo(implementedSet, 0);
                break;
        }
        int lengthArray = implementedSet.Length;
        Random rnd = new Random();
        int indexOfTarget = rnd.Next(0, lengthArray);
        this.target = this.implementedSet[indexOfTarget];
        this.implementedSet = this.implementedSet.Where(val => val != this.target).ToArray();

        int totalLength = 0;              //to get a suitible index
        foreach (char[] i in arraysToFill)
        {
            totalLength += i.Length;
        }

        int positionOfTarget = rnd.Next(0, totalLength);

        int totalIndex = 0;
        foreach (char[] i in arraysToFill)
        {
            for (int j = 0; j < i.Length; j++)
            {
                if (totalIndex == positionOfTarget && this.targetPresent)
                {
                    i[j] = this.target;
                }
                else
                {
                    i[j] = this.implementedSet[rnd.Next(0, this.implementedSet.Length)];
                }
                totalIndex++;
            }
        }
    }

    public char[][] getFilledArrays()
    {
        return this.arraysToFill;
    }

    public char getTarget()
    {
        return this.target;
    }
}
