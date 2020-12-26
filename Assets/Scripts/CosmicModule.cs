using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class CosmicModule : MonoBehaviour {

    //Bomb Sound and Info
    public KMAudio Audio;
    public KMBombInfo Bomb;
    public KMBombModule Module;

    //Buttons and Shit
    public KMSelectable[] NumberButtons;
    public KMSelectable SubmitButton;
    public KMSelectable CLRButton;
    public TextMesh DisplayText;
    public Animator[] MovingShit;
    public KMSelectable[] AllButtons;

    //Variables and Shit
    private string storedEntry = "";
    private int selectedNumber;
    private int expectedAnswer;
    static int moduleIdCounter = 1;
    int moduleId;
    bool isSolved;
    bool IHopeThisWorks;
    private float InteractionPunchIntensityModifier = .5f;

    private static readonly CosmicKey[] _AnswerKey = new CosmicKey[]
    {
        new CosmicKey { Displayed = 0, Answer = 4 },
        new CosmicKey { Displayed = 1, Answer = 3 },
        new CosmicKey { Displayed = 2, Answer = 3 },
        new CosmicKey { Displayed = 3, Answer = 5 },
        new CosmicKey { Displayed = 4, Answer = 4 },
        new CosmicKey { Displayed = 5, Answer = 4 },
        new CosmicKey { Displayed = 6, Answer = 3 },
        new CosmicKey { Displayed = 7, Answer = 5 },
        new CosmicKey { Displayed = 8, Answer = 5 },
        new CosmicKey { Displayed = 9, Answer = 4 },
        new CosmicKey { Displayed = 10, Answer = 3 }, //10
        new CosmicKey { Displayed = 11, Answer = 6 },
        new CosmicKey { Displayed = 12, Answer = 6 },
        new CosmicKey { Displayed = 13, Answer = 8 },
        new CosmicKey { Displayed = 14, Answer = 8 },
        new CosmicKey { Displayed = 15, Answer = 7 },
        new CosmicKey { Displayed = 16, Answer = 7 },
        new CosmicKey { Displayed = 17, Answer = 9 },
        new CosmicKey { Displayed = 18, Answer = 8 },
        new CosmicKey { Displayed = 19, Answer = 8 },
        new CosmicKey { Displayed = 20, Answer = 6 }, //20
        new CosmicKey { Displayed = 21, Answer = 9 },
        new CosmicKey { Displayed = 22, Answer = 9 },
        new CosmicKey { Displayed = 23, Answer = 11 },
        new CosmicKey { Displayed = 24, Answer = 10 },
        new CosmicKey { Displayed = 25, Answer = 10 },
        new CosmicKey { Displayed = 26, Answer = 9 },
        new CosmicKey { Displayed = 27, Answer = 11 },
        new CosmicKey { Displayed = 28, Answer = 11 },
        new CosmicKey { Displayed = 29, Answer = 10 },
        new CosmicKey { Displayed = 30, Answer = 6 }, //30
        new CosmicKey { Displayed = 31, Answer = 9 },
        new CosmicKey { Displayed = 32, Answer = 9 },
        new CosmicKey { Displayed = 33, Answer = 11 },
        new CosmicKey { Displayed = 34, Answer = 10 },
        new CosmicKey { Displayed = 35, Answer = 10 },
        new CosmicKey { Displayed = 36, Answer = 9 },
        new CosmicKey { Displayed = 37, Answer = 11 },
        new CosmicKey { Displayed = 38, Answer = 11 },
        new CosmicKey { Displayed = 39, Answer = 10 },
        new CosmicKey { Displayed = 40, Answer = 5 }, //40
        new CosmicKey { Displayed = 41, Answer = 8 },
        new CosmicKey { Displayed = 42, Answer = 8 },
        new CosmicKey { Displayed = 43, Answer = 10 },
        new CosmicKey { Displayed = 44, Answer = 9 },
        new CosmicKey { Displayed = 45, Answer = 9 },
        new CosmicKey { Displayed = 46, Answer = 8 },
        new CosmicKey { Displayed = 47, Answer = 10 },
        new CosmicKey { Displayed = 48, Answer = 10 },
        new CosmicKey { Displayed = 49, Answer = 9 },
        new CosmicKey { Displayed = 50, Answer = 5 }, //50
        new CosmicKey { Displayed = 51, Answer = 8 },
        new CosmicKey { Displayed = 52, Answer = 8 },
        new CosmicKey { Displayed = 53, Answer = 10 },
        new CosmicKey { Displayed = 54, Answer = 9 },
        new CosmicKey { Displayed = 55, Answer = 9 },
        new CosmicKey { Displayed = 56, Answer = 8 },
        new CosmicKey { Displayed = 57, Answer = 10 },
        new CosmicKey { Displayed = 58, Answer = 10 },
        new CosmicKey { Displayed = 59, Answer = 9 },
        new CosmicKey { Displayed = 60, Answer = 5 }, //60
        new CosmicKey { Displayed = 61, Answer = 8 },
        new CosmicKey { Displayed = 62, Answer = 8 },
        new CosmicKey { Displayed = 63, Answer = 10 },
        new CosmicKey { Displayed = 64, Answer = 9 },
        new CosmicKey { Displayed = 65, Answer = 9 },
        new CosmicKey { Displayed = 66, Answer = 8 },
        new CosmicKey { Displayed = 67, Answer = 10 },
        new CosmicKey { Displayed = 68, Answer = 10 },
        new CosmicKey { Displayed = 69, Answer = 9 },
        new CosmicKey { Displayed = 70, Answer = 7 }, //70
        new CosmicKey { Displayed = 71, Answer = 10 },
        new CosmicKey { Displayed = 72, Answer = 10 },
        new CosmicKey { Displayed = 73, Answer = 12 },
        new CosmicKey { Displayed = 74, Answer = 11 },
        new CosmicKey { Displayed = 75, Answer = 11 },
        new CosmicKey { Displayed = 76, Answer = 10 },
        new CosmicKey { Displayed = 77, Answer = 12 },
        new CosmicKey { Displayed = 78, Answer = 12 },
        new CosmicKey { Displayed = 79, Answer = 11 },
        new CosmicKey { Displayed = 80, Answer = 6 }, //80
        new CosmicKey { Displayed = 81, Answer = 9 },
        new CosmicKey { Displayed = 82, Answer = 9 },
        new CosmicKey { Displayed = 83, Answer = 11 },
        new CosmicKey { Displayed = 84, Answer = 10 },
        new CosmicKey { Displayed = 85, Answer = 10 },
        new CosmicKey { Displayed = 86, Answer = 9 },
        new CosmicKey { Displayed = 87, Answer = 11 },
        new CosmicKey { Displayed = 88, Answer = 11 },
        new CosmicKey { Displayed = 89, Answer = 10 },
        new CosmicKey { Displayed = 90, Answer = 6 }, //90
        new CosmicKey { Displayed = 91, Answer = 9 },
        new CosmicKey { Displayed = 92, Answer = 9 },
        new CosmicKey { Displayed = 93, Answer = 11 },
        new CosmicKey { Displayed = 94, Answer = 10 },
        new CosmicKey { Displayed = 95, Answer = 10 },
        new CosmicKey { Displayed = 96, Answer = 9 },
        new CosmicKey { Displayed = 97, Answer = 11 },
        new CosmicKey { Displayed = 98, Answer = 11 },
        new CosmicKey { Displayed = 99, Answer = 10 }
    };

    void Awake()
    {
        moduleId = moduleIdCounter++;

        SubmitButton.OnInteract += delegate { SubmitEntry(); return false; };
        CLRButton.OnInteract += delegate { ClearEntry(); return false; };

        for (int i = 0; i < AllButtons.Length; i++)
        {
            Animator anim = AllButtons[i].GetComponentInChildren<Animator>();
            string name = AllButtons[i].name;

            AllButtons[i].OnInteract += delegate ()
            {
                anim.SetTrigger("PushTrigger");
                return false;
            };
        }

        for (var i = 0; i < NumberButtons.Length; i++)
        {
            int j = i;
            NumberButtons[i].OnInteract += delegate { NumberInput(j); return false; };
        }
    }

    // Use this for initialization
    void Start ()
    {
        selectedNumber = Rnd.Range(0, 10000);
        DisplayText.text = selectedNumber.ToString();
	}

    void ClearEntry()
    {
        if (isSolved)
            return;

        Animator anim = MovingShit[11];
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, CLRButton.transform);
        CLRButton.AddInteractionPunch(InteractionPunchIntensityModifier);

        storedEntry = "";
        DisplayText.text = selectedNumber.ToString(); Debug.LogFormat("[Cosmic #{0}] Clearing Screen, Showing Current number {1}", moduleId, selectedNumber);
    }

    void SubmitEntry()
    {
        if (isSolved)
            return;

        Animator anim = MovingShit[10];
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, SubmitButton.transform);
        SubmitButton.AddInteractionPunch(InteractionPunchIntensityModifier);
        var WhatTheFUck = _AnswerKey[selectedNumber % 100];
        var Answer = WhatTheFUck.Answer;
        Debug.Log(Answer);
        if (selectedNumber > 999) {
          Debug.Log("Fat nuts");
          switch (selectedNumber / 1000) {
            case 1:
            Answer += 8 + 3;
            break;
            case 2:
            Answer += 8 + 3;
            break;
            case 3:
            Answer += 8 + 5;
            break;
            case 4:
            Answer += 8 + 4;
            break;
            case 5:
            Answer += 8 + 4;
            break;
            case 6:
            Answer += 8 + 3;
            break;
            case 7:
            Answer += 8 + 5;
            break;
            case 8:
            Answer += 8 + 5;
            break;
            case 9:
            Answer += 8 + 4;
            break;
          }
        }
        if (selectedNumber > 99) {
          Debug.Log("hi");
          switch ((selectedNumber % 1000 - selectedNumber % 100) / 100) {
            case 1:
            Answer += 7 + 3;
            break;
            case 2:
            Answer += 7 + 3;
            break;
            case 3:
            Answer += 7 + 5;
            break;
            case 4:
            Answer += 7 + 4;
            break;
            case 5:
            Answer += 7 + 4;
            break;
            case 6:
            Answer += 7 + 3;
            break;
            case 7:
            Answer += 7 + 5;
            break;
            case 8:
            Answer += 7 + 5;
            break;
            case 9:
            Answer += 7 + 4;
            break;
          }
        }
	if (selectedNumber % 100 == 0)
	  Answer -= 4;
        Debug.Log(Answer);
        if (storedEntry == "4" && Answer == 4)
        {
            if (Bomb.GetSolvedModuleNames().Count < Bomb.GetSolvableModuleNames().Count)
                Audio.PlaySoundAtTransform("solve", transform);
            isSolved = true;
            Module.HandlePass();
            Debug.LogFormat("[Cosmic #{0}] 4 is Cosmic. Module Solved.", moduleId);
        }
        else if (storedEntry == Answer.ToString())
        {
            Debug.LogFormat("[Cosmic #{0}] {1} is {2}. Continuing...", moduleId, selectedNumber, Answer);
            Audio.PlaySoundAtTransform("Select", transform);
            selectedNumber = Answer;
            DisplayText.text = selectedNumber.ToString();
            storedEntry = "";
        }
        else
        {
            Debug.LogFormat("[Cosmic #{0}] Displayed number is {1}. You submitted {2}. I wanted {3}. Strike!", moduleId, selectedNumber, storedEntry, Answer);
            Module.HandleStrike();
            DisplayText.text = selectedNumber.ToString();
            storedEntry = "";
        }
    }

    void NumberInput(int number)
    {
        if (isSolved)
            return;
        Animator anim = MovingShit[number];
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, NumberButtons[number].transform);
        NumberButtons[number].AddInteractionPunch(InteractionPunchIntensityModifier);

        if (storedEntry.Length == 2)
            return;

        storedEntry = storedEntry + number;
        Debug.LogFormat("[Cosmic #{0}] Currently Displaying {1}.", moduleId, storedEntry);
        DisplayText.text = storedEntry;
    }

#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use the command !{0} submit ## to submit a two/one digit number.";
#pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string Command)
    {
        yield return null;
        Command = Command.Trim().ToUpper();
        string[] Parameters = Command.Split(' ');
        yield return null;
        if (Parameters[0] != "SUBMIT" || Parameters.Length != 2)
            yield return "sendtochaterror I don't understand";
        else if (Parameters[1].Length > 2 || Parameters[1].Length == 0)
            yield return "sendtochaterror I don't understand";
        else if (!(Parameters[1].Any(x => "0123456789".Contains(x))))
            yield return "sendtochaterror I don't understand";
        else
        {
            for (int i = 0; i < Parameters[1].Length; i++)
            {
                NumberButtons[int.Parse(Parameters[1][i].ToString())].OnInteract();
                yield return new WaitForSeconds(.1f);
            }
            SubmitButton.OnInteract();
        }
    }

    IEnumerator TwitchHandleForceSolve()
    {
        while (!isSolved)
        {
            var WhatTheFUck = _AnswerKey[selectedNumber % 100];
            var AutosolveMethod = WhatTheFUck.Answer;
            if (selectedNumber > 999)
            {
                switch (selectedNumber / 1000)
                {
                    case 1:
                        AutosolveMethod += 8 + 3;
                        break;
                    case 2:
                        AutosolveMethod += 8 + 3;
                        break;
                    case 3:
                        AutosolveMethod += 8 + 5;
                        break;
                    case 4:
                        AutosolveMethod += 8 + 4;
                        break;
                    case 5:
                        AutosolveMethod += 8 + 4;
                        break;
                    case 6:
                        AutosolveMethod += 8 + 3;
                        break;
                    case 7:
                        AutosolveMethod += 8 + 5;
                        break;
                    case 8:
                        AutosolveMethod += 8 + 5;
                        break;
                    case 9:
                        AutosolveMethod += 8 + 4;
                        break;
                }
            }
            if (selectedNumber > 99)
            {
                switch ((selectedNumber % 1000 - selectedNumber % 100) / 100)
                {
                    case 1:
                        AutosolveMethod += 7 + 3;
                        break;
                    case 2:
                        AutosolveMethod += 7 + 3;
                        break;
                    case 3:
                        AutosolveMethod += 7 + 5;
                        break;
                    case 4:
                        AutosolveMethod += 7 + 4;
                        break;
                    case 5:
                        AutosolveMethod += 7 + 4;
                        break;
                    case 6:
                        AutosolveMethod += 7 + 3;
                        break;
                    case 7:
                        AutosolveMethod += 7 + 5;
                        break;
                    case 8:
                        AutosolveMethod += 7 + 5;
                        break;
                    case 9:
                        AutosolveMethod += 7 + 4;
                        break;
                }
            }
            for (int i = 0; i < AutosolveMethod.ToString().Length; i++)
            {
                NumberButtons[int.Parse(AutosolveMethod.ToString()[i].ToString())].OnInteract();
                yield return new WaitForSeconds(.1f);
            }
            SubmitButton.OnInteract();
            yield return new WaitForSeconds(.1f);
        }
    }
}

sealed class CosmicKey
{
    public int Displayed;
    public int Answer;
}
