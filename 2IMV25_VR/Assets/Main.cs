using UnityEngine;

public class Main : MonoBehaviour
{

    public static ProblemSet ps;


    // Use this for initialization
    void Awake()
    {
        ps = new ProblemSet(ProblemSet.CharSet.B, true);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void nextSituation()
    {

    }
}
