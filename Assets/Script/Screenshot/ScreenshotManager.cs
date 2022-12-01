using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenshotManager : MonoBehaviour
{
    ScreenshotMouseOrbit mouseOrbit;

    [SerializeField] GameObject groupOrganic, groupAnorganic, groupB3, groupPaper, groupResidue;

    public GameObject[] objectOrganic;
    public GameObject[] objectAnorganic;
    public GameObject[] objectB3;
    public GameObject[] objectPaper;
    public GameObject[] objectResidue;

    int seqOrganic, seqAnorganic, seqB3, seqPaper, seqResidue;

    enum ActiveGroup
    {
        Organic,
        Anorganic,
        B3,
        Paper,
        Residue,
    }

    ActiveGroup activeGroup = ActiveGroup.Organic;



    private void Start()
    {
        seqOrganic = seqAnorganic = seqB3 = seqPaper = seqResidue = 0;
    }



    void Update()
    {
        if (Keyboard.current[Key.Digit1].wasPressedThisFrame)
        {
            activeGroup = ActiveGroup.Organic;
            groupOrganic.SetActive(true);
            groupAnorganic.SetActive(false);
            groupB3.SetActive(false);
            groupPaper.SetActive(false);
            groupResidue.SetActive(false);
        }
        else if (Keyboard.current[Key.Digit2].wasPressedThisFrame)
        {
            activeGroup = ActiveGroup.Anorganic;
            groupOrganic.SetActive(false);
            groupAnorganic.SetActive(true);
            groupB3.SetActive(false);
            groupPaper.SetActive(false);
            groupResidue.SetActive(false);
        }
        else if (Keyboard.current[Key.Digit3].wasPressedThisFrame)
        {
            activeGroup = ActiveGroup.B3;
            groupOrganic.SetActive(false);
            groupAnorganic.SetActive(false);
            groupB3.SetActive(true);
            groupPaper.SetActive(false);
            groupResidue.SetActive(false);
        }
        else if (Keyboard.current[Key.Digit4].wasPressedThisFrame)
        {
            activeGroup = ActiveGroup.Paper;
            groupOrganic.SetActive(false);
            groupAnorganic.SetActive(false);
            groupB3.SetActive(false);
            groupPaper.SetActive(true);
            groupResidue.SetActive(false);
        }
        else if (Keyboard.current[Key.Digit5].wasPressedThisFrame)
        {
            activeGroup = ActiveGroup.Residue;
            groupOrganic.SetActive(false);
            groupAnorganic.SetActive(false);
            groupB3.SetActive(false);
            groupPaper.SetActive(false);
            groupResidue.SetActive(true);
        }

        if (Keyboard.current[Key.E].wasPressedThisFrame)
        {
            SwitchTarget(true);
        }
        else if (Keyboard.current[Key.Q].wasPressedThisFrame)
        {
            SwitchTarget(false);
        }
    }



    void SwitchTarget(bool next)
    {
        switch (activeGroup)
        {
            case ActiveGroup.Organic:
                objectOrganic[seqOrganic].SetActive(false);
                seqOrganic = next ? seqOrganic += 1 : seqOrganic -= 1;
                if (seqOrganic < 0) seqOrganic = objectOrganic.Length - 1;
                if (seqOrganic >= objectOrganic.Length) seqOrganic = 0;
                objectOrganic[seqOrganic].SetActive(true);
                break;
            case ActiveGroup.Anorganic:
                objectAnorganic[seqAnorganic].SetActive(false);
                seqAnorganic = next ? seqAnorganic += 1 : seqAnorganic -= 1;
                if (seqAnorganic < 0) seqAnorganic = objectAnorganic.Length - 1;
                if (seqAnorganic >= objectAnorganic.Length) seqAnorganic = 0;
                objectAnorganic[seqAnorganic].SetActive(true);
                break;
            case ActiveGroup.B3:
                objectB3[seqB3].SetActive(false);
                seqB3 = next ? seqB3 += 1 : seqB3 -= 1;
                if (seqB3 < 0) seqB3 = objectB3.Length - 1;
                if (seqB3 >= objectB3.Length) seqB3 = 0;
                objectB3[seqB3].SetActive(true);
                break;
            case ActiveGroup.Paper:
                objectPaper[seqPaper].SetActive(false);
                seqPaper = next ? seqPaper += 1 : seqPaper -= 1;
                if (seqPaper < 0) seqPaper = objectPaper.Length - 1;
                if (seqPaper >= objectPaper.Length) seqPaper = 0;
                objectPaper[seqPaper].SetActive(true);
                break;
            case ActiveGroup.Residue:
                objectResidue[seqResidue].SetActive(false);
                seqResidue = next ? seqResidue += 1 : seqResidue -= 1;
                if (seqResidue < 0) seqResidue = objectResidue.Length - 1;
                if (seqResidue >= objectResidue.Length) seqResidue = 0;
                objectResidue[seqResidue].SetActive(true);
                break;
        }
    }
}
