using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ZoomInController : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    private bool wasTracked = false;

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            if (!wasTracked)
            {
                GameObject game = GameObject.FindGameObjectWithTag("GameBoard");
                game.transform.localScale += new Vector3(1, 1, 1);
                wasTracked = true;
            }
        }
        else
        {
            if (wasTracked)
            {
                GameObject game = GameObject.FindGameObjectWithTag("GameBoard");
                game.transform.localScale = new Vector3(1, 1, 1);
                wasTracked = false;
            }
        }
    }
}