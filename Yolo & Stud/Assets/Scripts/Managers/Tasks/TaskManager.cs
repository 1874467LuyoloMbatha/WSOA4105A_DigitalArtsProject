using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TaskManager : Singleton<TaskManager>
{
    [Header("External References")]
    [SerializeField] List<TaskGameObject> tasks = new List<TaskGameObject>();

    [Header("Unity Handles")]
    [Tooltip("Drag The ToDo parent prefab here")]
    [SerializeField] GameObject parentTaskManager;
    [Tooltip("Drag The content game object under Canvas. This will allow us to populate our ToDo list")]
    [SerializeField] Transform contentGameObject;
    [Tooltip("Drag The ToDo prefab here")]
    [SerializeField] GameObject toDoPrefab;
    [SerializeField] InputField inputFields; //for the name and type
    [SerializeField] Button temmpBtn;

    //TO EDIT LATER
    [Header("Storing Data")]
    string filePath;  //Path to store data

    //Thiss isss only used for the swithing button function
    [Header("Button Funtionality Variables")]
    bool active;


    void Start()
    {

        //Stores?Locates file in the user's computer
        filePath = Application.persistentDataPath + "/TaskList.txt";
        LoadTaskData();

        //makes it possible to open/add a task
        temmpBtn.onClick.AddListener(delegate { AddTask(); });

        if(parentTaskManager != null)
             parentTaskManager.SetActive(false);
    }
    #region Old & Unused 
    //Old Version
    void AddTaskOld(string name, int loadIndex = 0, bool load = false)
    {
        GameObject taskItem = Instantiate(toDoPrefab); //creates it
        taskItem.transform.SetParent(contentGameObject); //puts it under the content gameobject

        //
        int index = loadIndex;

        if (!load)
            index = tasks.Count;

        //Assign all the data given to the specific item made
        TaskGameObject taskItemObj = taskItem.GetComponent<TaskGameObject>();
        taskItemObj.SetTaskInfo(name, index);

        //Add it to our Array/List
        tasks.Add(taskItemObj);

        //Ensure that tthe task set is the one the user interacts with 
        TaskGameObject temp = taskItemObj;
        taskItemObj.GetComponent<Toggle>().onValueChanged.AddListener(delegate { TaskCheckMark(temp); });

        //Save
        if (!load)
            SaveTaskData();

        //Swwith on Panel maybe...
    }
	#endregion

	#region Used outside
	public void AddToList(TaskGameObject obj)
    {
        // obj.SetTaskInfo(ta, index);
        tasks.Add(obj);
    }
    public void TaskCheckMark(TaskGameObject obj)
    {
        if (SettingsMenu.Instance.GetTaskMode())
        {
            //Remove from list, delete it
            tasks.Remove(obj);

            //Save file before destroying
            SaveTaskData();

            Destroy(obj.gameObject);
        }
        else
		{
            obj.taskBg.color = obj.finishedColour;
		}

        //Save file
        SaveTaskData();
    }
	#endregion

	#region Asssigned In Inspector
	//New Version
	public void AddTask(string name = " ", int loadIndex = 0, bool load = false)
	{
        GameObject taskItem = Instantiate(toDoPrefab); //creates it
        taskItem.transform.SetParent(contentGameObject); //puts it under the content gameobject
        taskItem.transform.localScale = Vector3.one;

        if(load)
		{
            taskItem.GetComponent<TaskGameObject>().SetTaskInfo(name, loadIndex);
		}
        //
        int index = loadIndex;

        if (!load)
        {
            Debug.Log("Added New Task!");
            index = tasks.Count;
            Debug.Log("Current Add Index: " + index);
            SaveTaskData();
        }
    }
    //For Gameobjeccts to swith on and off
    public void ButtonSwitch(GameObject obj)
    {
        active = !active;
        obj.SetActive(active);
    }
    #endregion

    //May be removveds
    #region Saving Logic

    public void SaveTaskData() 
    {
        //Check if this file exists then override (ight remove the if)
        string contents = "";

		for (int i = 0; i < tasks.Count; i++)
		{
            TaskList tl = new TaskList(tasks[i].gameObjectName, tasks[i].taskIndex);
            contents += JsonUtility.ToJson(tl) + "\n";
            Debug.Log("Interation " + i);
		}

        //save
        File.WriteAllText(filePath, contents);
        Debug.Log("Save Process Completed!");
    }

    void LoadTaskData()
	{
        //checck if we have saved data
        if (File.Exists(filePath))
        {
            string contents = File.ReadAllText(filePath);

            string[] splitContents = contents.Split('\n');

			foreach (var item in splitContents)
			{
                if (item.Trim() != "")
                {
                   // Debug.Log(item);
                    TaskList obj = JsonUtility.FromJson<TaskList>(item);
                    AddTask(obj.gameObjectName, obj.taskIndex, true);
                }
            }

        }
        else
            Debug.LogError(filePath + " DOES NOT Exists");
    }

    //temp cclassss
    public class TaskList
	{
        public string gameObjectName;
        public int taskIndex;

        public TaskList(string name, int indcx)
        {
            this.gameObjectName = name;
            this.taskIndex = indcx;
        }
    }
	#endregion

}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/