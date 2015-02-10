using UnityEngine;
using System.Collections;

/*
 * A public interface for a Timer object
 * A Timer can be set to any float value greater than 0
 * A timer must be set before first use. Then, it can be
 *  started, paused, resumed, restarted, and stopped.
 * Timers can also be set to different values dynamically
 */
public class Timer : MonoBehaviour
{
	/* -----Variables----- */

	// Value that the timer starts at when TStart() or TRestart() is called
	public float		StartTime
	{
		get 		{ return StartTime; }
		private set { StartTime = value; }
	}

	// True if the timer is currently running
	public bool			Running { get; private set; }
	// Current running value of the timer
	public float		CurrentTime
	{
		get
		{
			if (CurrentTime < 0f) return 0f;
			return CurrentTime;
		}
		private set { CurrentTime = value; }
	}

	/* -----Unity methods-----*/

	void Start()
	{
		Running = false;
		StartTime = -1f;
	}

	void Update()
	{
		if (!Running)
			return;

		// Decrement timer if running
		// When CurrentTime hits 0, timer is no longer running
		CurrentTime -= Time.deltaTime;
		if (CurrentTime <= 0f)
			Running = false;
	}

	/* -----Custom methods----- */

	// Set the starting value for the timer
	// If the timer is stopped or reset, this value remains until overwritten
	// A Timer cannot be set while it is currently running
	public void TSet(float _val)
	{
		if (_val < 0f)
		{
			Debug.LogError("Cannot set timer to value <= 0", this.gameObject);
			return;
		}
		else if (Running)
		{
			Debug.LogError("Cannot set timer while it is running", this.gameObject);
			return;
		}
		StartTime = _val;
	}

	// Start running the timer
	// If timer is already running, do nothing
	//  (Use TRestart() if you wish to restart timer while it is running)
	public void TStart()
	{
		if (!CheckConditions())
			return;
		else if (Running)
			return;

		Running = true;
		CurrentTime = StartTime;
	}

	// Pause the timer at the current value.
	// Use TResume() to resume from current time
	public void TPause()
	{
		Running = false;
	}

	// Resume the timer if it is paused
	// If it was not paused, a warning is printed and nothing happens
	public void TResume()
	{
		if (!CheckConditions())
			return;
		Running = true;
	}

	// Stop the timer from running
	// Sets the timer's value to 0
	public void TStop()
	{
		Running = false;
		CurrentTime = 0f;
	}

	// Restart the timer (even if not running)
	public void TRestart()
	{
		if (!CheckConditions())
			return;
		Running = true;
		CurrentTime = StartTime;
	}

	// Returns true if called timer operation is legal
	// Example: A timer cannot run if it was never set
	private bool CheckConditions()
	{
		if (StartTime == -1f)
		{
			Debug.LogError("Cannot start an uninitialized timer", this.gameObject);
			return false;
		}
		return true;
	}
}