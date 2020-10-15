using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileNameController : MonoBehaviour
{	
	public DataExporter dataExporter;
	
    void FixedUpdate()
    {
		var filePath = dataExporter.filepath;
		
		// Update the text to the filepath
		gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = filePath.ToString();
    }
}
