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

    private char target;
    protected Boolean targetPresent;
    private char[] implementedSet;
    private int indexOfTarget;
    protected char[][] arraysToFill;
    public int totalLength;

    public ProblemSet(CharSet chosenSet, Boolean target)
    {
        this.arraysToFill = new char[6][];
        this.arraysToFill[0] = new char[36];
        this.arraysToFill[1] = new char[36];
        this.arraysToFill[2] = new char[36];
        this.arraysToFill[3] = new char[36];
        this.arraysToFill[4] = new char[25];
        this.arraysToFill[5] = new char[25];

        this.targetPresent = target;

        switch (chosenSet)
        {
            case CharSet.A:
                this.implementedSet = new char[setA.Length];
                setA.CopyTo(implementedSet, 0);
                break;
            case CharSet.B:
                this.implementedSet = new char[setB.Length];
                setB.CopyTo(implementedSet, 0);
                break;
        }
        int lengthArray = implementedSet.Length;
        Random rnd = new Random();
        indexOfTarget = rnd.Next(0, lengthArray);
        this.target = this.implementedSet[indexOfTarget];
        this.implementedSet = this.implementedSet.Where(val => val != this.target).ToArray();
       

        totalLength = 0;              //to get a suitible index
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
                    int random = rnd.Next(0, this.implementedSet.Length);
                    i[j] = this.implementedSet[random];
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
