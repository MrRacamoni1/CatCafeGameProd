using UnityEngine;
using TMPro;

public enum TaskStep
{
    None,
    MuffinGather,
    MuffinOven,
    MuffinCollect,
    PuddingGather,
    PuddingPot,
    PuddingCollect,
    Exit
}

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    [SerializeField] TMP_Text taskListText;
    [SerializeField] ShiftEndZone shiftEndZone;

    TaskStep[] muffinSteps = { TaskStep.MuffinGather, TaskStep.MuffinOven, TaskStep.MuffinCollect };
    TaskStep[] puddingSteps = { TaskStep.PuddingGather, TaskStep.PuddingPot, TaskStep.PuddingCollect };

    bool[] completed;

    void Awake()
    {
        Instance = this;
        completed = new bool[System.Enum.GetValues(typeof(TaskStep)).Length];
    }

    void Start()
    {
        if (taskListText == null) Debug.LogError("TaskManager: the Task List Text slot is empty!");
        if (shiftEndZone == null) Debug.LogError("TaskManager: the Shift End Zone slot is empty!");

        UpdateDisplay();
    }

    public void CompleteStep(TaskStep step)
    {
        if (step == TaskStep.None) return;
        if (completed[(int)step]) return;

        completed[(int)step] = true;
        UpdateDisplay();

        if (AllComplete(muffinSteps) && AllComplete(puddingSteps))
        {
            shiftEndZone.Activate();
        }
    }

    bool IsComplete(TaskStep step)
    {
        return completed[(int)step];
    }

    bool AllComplete(TaskStep[] steps)
    {
        foreach (TaskStep step in steps)
        {
            if (!IsComplete(step)) return false;
        }
        return true;
    }

    string GetDescription(TaskStep step)
    {
        switch (step)
        {
            case TaskStep.MuffinGather:    return "Gather the muffin ingredients";
            case TaskStep.MuffinOven:      return "Put the ingredients in the oven";
            case TaskStep.MuffinCollect:   return "Collect the muffin";
            case TaskStep.PuddingGather:   return "Gather the rice pudding ingredients";
            case TaskStep.PuddingPot:      return "Put the ingredients in the pot";
            case TaskStep.PuddingCollect:  return "Collect the rice pudding";
            default:                       return "";
        }
    }

    string Strike(string text, bool done)
    {
        if (done) return "<s>" + text + "</s>";
        return text;
    }

    string BuildGroup(string title, TaskStep[] steps)
    {
        string lines = "";

        foreach (TaskStep step in steps)
        {
            lines += "      " + Strike("- " + GetDescription(step), IsComplete(step)) + "\n";
        }

        return "<b>" + Strike(title, AllComplete(steps)) + "</b>\n" + lines + "\n";
    }

    void UpdateDisplay()
    {
        if (taskListText == null) return;

        string display = BuildGroup("1. Make the muffin", muffinSteps)
                       + BuildGroup("2. Make the rice pudding", puddingSteps)
                       + "<b>" + Strike("3. Leave the cafe", IsComplete(TaskStep.Exit)) + "</b>";

        taskListText.text = display;
    }
}