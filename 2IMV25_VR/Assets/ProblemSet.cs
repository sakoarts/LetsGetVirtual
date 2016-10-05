using System;
using System.Collections;
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
    public Boolean targetPresent;
    private char[] implementedSet;
    private int indexOfTarget;
    protected char[][] arraysToFill;
    private int totalLength;
    private string targetLocation;
    private char setUsed;
    public ProblemSet(CharSet chosenSet, Boolean target)
    {
        this.arraysToFill = new char[6][];
        this.arraysToFill[0] = new char[36];
        this.arraysToFill[1] = new char[36];
        this.arraysToFill[2] = new char[36];
        this.arraysToFill[3] = new char[36];
        this.arraysToFill[4] = new char[20];
        this.arraysToFill[5] = new char[20];

        this.targetPresent = target;

        switch (chosenSet)
        {
            case CharSet.A:
                this.implementedSet = new char[setA.Length];
                setA.CopyTo(implementedSet, 0);
                setUsed = 'A';
                break;
            case CharSet.B:
                this.implementedSet = new char[setB.Length];
                setB.CopyTo(implementedSet, 0);
                setUsed = 'B';
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
        //   foreach (char[] i in arraysToFill)
        for(int p =0; p < arraysToFill.Length; p++)
        {
            ArrayList dontFill = new ArrayList();
            for (int j = 0; j < arraysToFill[p].Length; j++)
            { 
                switch (p)
                {      
                    case 0:
                        //Deur
                        dontFill.Add(3);
                        dontFill.Add(9);
                        dontFill.Add(15);
                        dontFill.Add(21);
                        break;
                    case 1:
                        dontFill.Add(15);
                        dontFill.Add(21);
                        break;
                    case 2:
                        break;
                    case 3:
                        dontFill.Add(14);
                        dontFill.Add(20);
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                }

                if (dontFill.Contains(j))
                {
                    if (totalIndex == positionOfTarget && this.targetPresent)
                    {
                        positionOfTarget++;
                    }
                }
                else { 
                    if (totalIndex == positionOfTarget && this.targetPresent)
                    {
                        switch (p)
                        {
                            case 0:
                                targetLocation = "Deur /North";
                                break;
                            case 1:
                                targetLocation = "Raam /East";
                                break;
                            case 2:
                                targetLocation = "Leeg / South";
                                break;
                            case 3:
                                targetLocation = "Raam /West";
                                break;
                            case 4:
                                targetLocation = "Floor";
                                break;
                            case 5:
                                targetLocation = "Ceiling";
                                break;
                        }
                        arraysToFill[p][j] = this.target;
                    }
                    else
                    {
                        int random = rnd.Next(0, this.implementedSet.Length);
                        arraysToFill[p][j] = this.implementedSet[random];
                    }
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

    public string returnExercise()
    {
        String str = "";
        str += targetPresent + ", " +target +"," +targetLocation + "," + setUsed + ",";
        return str;
    }
}
