using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class Saver : MonoBehaviour
{
    // Overall class that concentrates all mathods for saving data.

    public void SaveData(List<string> data, string directory, string filename)
    {
        // It saves participants' data during performing the task.
        string whereToSave = directory + filename;
        StreamWriter writer = new StreamWriter(whereToSave);
        using (writer)
        {
            int len = data.Count;
            for (int j = 0; j < len; j++)
            {
                writer.WriteLine(data[j]);
            }
        }
    }

    public void SaveSession(int trial, string directory, string filename)
    {
        // It saves the trial counter in a .txt file.
        string whereToSave = directory + filename;
        StreamWriter writer = new StreamWriter(whereToSave);
        using (writer)
        {
            writer.WriteLine(trial);
        }
    }

    public void CreateFile(string directory, string filename)
    {
        // It creates files to save participants' code and control experiments with many sessions.
        string whereToSave = directory + filename;
        StreamWriter writer = new StreamWriter(whereToSave);
        using (writer)
        {
                writer.WriteLine(1);
        }
    }

    /*public void CodeParticipant(string participant, string directory, string filename)
    {
        // It searches for a specific file's and participant's names.

        int snum;
        List<string> list = new List<string>();
        string whereToSave = directory + filename;
        bool fExists = File.Exists(whereToSave);
        
        if (fExists == true)
        {
            list = GetComponent<Loader>().ParticipantsList(directory, filename);

            if (list.Contains(participant) == true)
            {
                snum = list.IndexOf(participant) + 1;
            }
            else
            {
                snum = list.Count + 1;
            }
        }
        else
        {
            CreateFile(directory, filename);
            snum = 1;
        }
    }*/

    public void ParticipantData(string ID, string number, string major, string age, string directory, string filename)
    {
        // It saves participants' personal information.
        string builder = ID + "\t" + number + "\t" + major + "\t" + age;
        string whereToSave = directory + filename;
        StreamWriter writer = new StreamWriter(whereToSave);
        using (writer)
        {
            writer.WriteLine(builder, true);
        }
    }
}
