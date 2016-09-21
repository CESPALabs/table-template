 //  VSS $Header: /PiDevTools11/Inc/PDIg4.h 18    1/09/14 1:05p Suzanne $  

// attach to Polhemus object on calibration scene. ensures that stream is not destroyed between scenes
using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System;
using System.Text;
using System.Net;
using System.Threading;
using System.IO;
using System.Collections.Generic;


public enum PlTracker
{
    Liberty,
    Patriot,
    G4,
    Fastrak,
};

public class PlStream : MonoBehaviour
{
    // port used for our UDP connection
    public int port = 5123;
	public bool stopListening;

    // tracker descriptors
    public PlTracker tracker_type = PlTracker.Liberty;
    public int max_systems = 1;
    public int max_sensors = 4;

    // slots used to store tracker output data
    public bool[] active;
    public uint[] digio;
	public int nSenID;
    public Vector4[] positions;
    public Vector4[] orientations;

	public List<Vector4> posData = new List<Vector4>();
	public List<Vector4> rotData = new List<Vector4>();


    // internal state
    private int max_slots;
    private UdpClient udpClient;
    public Thread conThread;

	public int buffer_line;

    // Use this for initialization
    void Awake()
    {
		DontDestroyOnLoad(transform.gameObject);
		//stopListening = GameObject.Find("
		try
        {
            // there are some constraints between tracking systems
            switch (tracker_type)
            {
                case PlTracker.Liberty:
                    // liberty is a single tracker system
                    max_systems = (max_systems > 1) ? 1 : max_systems;
                    max_sensors = (max_sensors > 16) ? 16 : max_sensors;
                    break;
                case PlTracker.Patriot:
                    max_systems = (max_systems > 1) ? 1 : max_systems;
                    max_sensors = (max_sensors > 2) ? 2 : max_sensors;
                    break;
                case PlTracker.G4:
                    // all G4 hubs (systems) have a maximum of 3 sensors
                    max_sensors = (max_sensors > 3) ? 3 : max_sensors;
                    break;
                case PlTracker.Fastrak:
                    max_systems = (max_systems > 1) ? 1 : max_systems;
                    max_sensors = (max_sensors > 4) ? 4 : max_sensors;
                    break;
                default:
                    throw new Exception("[polhemus] Unknown Tracker selected in PlStream::Awake().");
            }

            // set the number of slots
            max_slots = max_sensors * max_systems;

            // allocate resources for those slots
            active = new bool[max_slots];
            digio = new uint[max_slots];
            positions = new Vector4[max_slots];
            orientations = new Vector4[max_slots];

            // initialize the slots
            for (int i = 0; i < max_slots; ++i)
            {
                active[i] = false;
                digio[i] = 0;
                positions[i] = Vector3.zero;
                orientations[i] = Vector4.zero;
            }

            switch (tracker_type)
            {
                case PlTracker.Liberty:
                case PlTracker.Patriot:
                    conThread = new Thread(new ThreadStart(read_liberty));
                    break;
               
                default:
                    throw new Exception("[polhemus] Unknown Tracker selected in PlStream::Awake().");
            }

            // start the read thread
            conThread.Start();

        } catch (Exception e)
        {
            Debug.Log(e);
            Debug.Log("[polhemus] PlStream terminated in PlStream::Awake().");
            Console.WriteLine("[polhemus] PlStream terminated in PlStream::Awake().");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // read thread

    // read thread
    public void read_liberty()
    {
        stopListening = false;

        udpClient = new UdpClient(port);
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, port);

        try
        {
            // create temp_active to mark slots
            bool[] temp_active = new bool[max_slots];

            // using hdr + pos + qtrn frame configuration for now
            while (!stopListening)
            {
                byte[] receiveBytes = udpClient.Receive(ref groupEP);

                // set slots to inactive
                for (var i = 0; i < max_slots; ++i)
                    temp_active[i] = false;

                // offset into buffer
                int offset = 0;
                while (offset + 36 <= receiveBytes.Length)
                {
                    // process header (8 bytes)
                    nSenID = System.Convert.ToInt32(receiveBytes[offset + 2]) - 1;
                    offset += 8;

                    if (nSenID > max_slots)
                    {
                        Console.WriteLine("[polhemus] SenID is greater than" + max_sensors.ToString() + ".");
                        throw new Exception("[polhemus] SenID is greater than" + max_sensors.ToString() + ".");
                    }

                    // process stylus (4 bytes)
                    //uint bfStylus = BitConverter.ToUInt32(receiveBytes, offset);
                    //offset += 4;

                    // process position (12 bytes)
                    float t = BitConverter.ToSingle(receiveBytes, offset);
                    float u = BitConverter.ToSingle(receiveBytes, offset + 4);
                    float v = BitConverter.ToSingle(receiveBytes, offset + 8);
                    offset += 12;

                    // process orientation (16 bytes)
                    float w = BitConverter.ToSingle(receiveBytes, offset);
                    float x = BitConverter.ToSingle(receiveBytes, offset + 4);
                    float y = BitConverter.ToSingle(receiveBytes, offset + 8);
                    float z = BitConverter.ToSingle(receiveBytes, offset + 12);
                    offset += 16;

                    // store results
                    temp_active[nSenID] = true;
                    //digio[nSenID] = bfStylus;
					positions[nSenID] = new Vector4(t, u, v, nSenID*1.0f);
                    orientations[nSenID] = new Vector4(w, x, y, z);

					posData.Add(positions[nSenID]);
					rotData.Add(orientations[nSenID]);

					buffer_line++;

                }

                // mark active slots
                for (var i = 0; i < max_slots; ++i)
                    active[i] = temp_active[i];
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            Debug.Log("[polhemus] PlStream terminated in PlStream::read_liberty()");
            Console.WriteLine("[polhemus] PlStream terminated in PlStream::read_liberty().");
        }
        finally
        {
            udpClient.Close();
            udpClient = null;
        }
    }


    // read thread

    // cleanup
    private void OnApplicationQuit()
    {
        try
        {
            // signal shutdown
            stopListening = true;
            
            // attempt to join for 500ms
            if (!conThread.Join(500))
            {
                // force shutdown
                conThread.Abort();
                if (udpClient != null)
                {
                    udpClient.Close();
                    udpClient = null;
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            Debug.Log("[polhemus] PlStream was unable to close the connection thread upon application exit. This is not a critical exception.");
        }
    }
}