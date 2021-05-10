using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MyMessageListener : MonoBehaviour
{
    //private bool isGO = false;
    // Start is called before the first frame update
    public SerialController serialController;
    void Start()
    {
        serialController = this.gameObject.GetComponent<SerialController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        float z = this.transform.position.z;
        this.transform.DOMove(new Vector3(x, y, z + 0.1f), 0.1f);
    }

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        Debug.Log("Arrived: " + msg);

        float x = this.transform.position.x;
        float y = this.transform.position.y;
        float z = this.transform.position.z;

        if (!msg.Contains("_")&&msg!="")
        {
            switch (int.Parse(msg))
            {
                case 1:
                    this.transform.DOMove(new Vector3(1f + x, y, z), 1f);
                    break;
                case 2:
                    this.transform.DOMove(new Vector3(-1f + x, y, z), 1f);
                    break;
                case 3:
                    this.transform.DOMove(new Vector3(x, 1f + y, z), 1f);
                    break;
                case 4:
                    this.transform.DOMove(new Vector3(x, -1f + y, z), 1f);
                    break;
            }

        }
        else if (msg != "")
        {
            if (msg[0] == 'y')
            {
                int x_input = int.Parse(msg.Substring(2))-512;
                this.transform.DOMove(new Vector3(x+x_input / 100, y, z), 1f);
            }

            if (msg[0] == 'x')
            {
                int y_input = int.Parse(msg.Substring(2))-512;
                this.transform.DOMove(new Vector3(x, y+y_input/100, z), 1f);
            }
        }
    }
    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }

    public void SendRED()
    {
        serialController.SendSerialMessage("R");
    }

    public void SendBlue()
    {
        serialController.SendSerialMessage("B");
    }

    public void SendGreen()
    {
        serialController.SendSerialMessage("G");
    }
}
