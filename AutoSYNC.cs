/**
 * AutoSYNC by lRaulMN7 Created 3/30/2019 
 **/

using System;
using System.Collections.Generic;
using System.Windows.Forms;

using ScriptPortal.Vegas;

public class EntryPoint {

    Vegas myVegas;
	
	public void FromVegas(Vegas vegas) {
    Project proj = vegas.Project;
        foreach (Track track in proj.Tracks)
        {		
            foreach (TrackEvent trackEvent in track.Events)
            {	
				VideoEvent vevnt = (VideoEvent)trackEvent;
				Envelopes vevntEnv = vevnt.Envelopes;
				MessageBox.Show(vevntEnv.HasEnvelope(EnvelopeType.Velocity).ToString());
            }
        }
	}
}
