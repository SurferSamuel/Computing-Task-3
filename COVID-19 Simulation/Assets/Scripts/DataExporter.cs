using UnityEngine;
using System; 
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
	
public class DataExporter : MonoBehaviour
{
	
	private string pathOriginName = "SimulationResults";
	public string filepath = "";
	
	private int testNum = 0;
	
	void Start()
	{
		// While loop to check for files
		while (true)
		{	
			// If file with name already exists
			if (File.Exists(pathOriginName + testNum + ".csv"))
			{
				// Add one to the num count
				testNum += 1;
			}
			else
			{	
				// If file doesn't exists, use this pathName
				filepath = pathOriginName + testNum + ".csv";
				
				// End while loop
				break;
			}
		}

        // Add titles to columns
        addColumnTitle();
	}

    void addColumnTitle()
    {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
        {
            // Write in line
            file.WriteLine("Time Stamp" + "," + "Infected" + "," + "Dead" + "," + "Recovered" + "," + "Susceptible");
        }
    }

	public void addRecord(string timeStamp, string numInfected, string numDead, string numRecovered, string numRecovnumNotInfectedered)
	{
		try
		{
			// Open to new line in file
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
			{
				// Write in line
				file.WriteLine(timeStamp + "," + numInfected + "," + numDead + "," + numRecovered + "," + numRecovnumNotInfectedered);
			}
		}

		// If 'try' returns an error
		catch(Exception ex)
		{
			// Show error in console
			Debug.Log("Error in DataExporter: " + ex);
		}
	}

    public void OpenSimulationResults()
    {
		// Open results file when simulation is ended
		
		// If platform is Windows
		if(Application.platform == RuntimePlatform.WindowsEditor)
		{
			Application.OpenURL(filepath);
		}
			
		// If platform is Mac
		if(Application.platform == RuntimePlatform.OSXEditor)
		{
			System.Diagnostics.Process.Start(filepath);
		}
    }
}
