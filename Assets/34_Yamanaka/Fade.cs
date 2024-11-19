using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    float m_startTime;

    public int m_fadeTime;
    Color m_alpha;
    string m_fadeStart;

    // Start is called before the first frame update
    void Start()
    {
        m_startTime = Time.time;
        m_fadeStart = "FadeIn";
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_fadeStart)
        {
            case "FadeIn":
                m_alpha.a = 1.0f - 
        }
    }
}
