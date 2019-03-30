/**
 * AutoSYNC by lRaulMN7 Created 3/30/2019 
 **/

using System;
using System.Collections.Generic;
using System.Windows.Forms;

using ScriptPortal.Vegas;

public class EntryPoint {

	public void FromVegas(Vegas vegas) {
	Vegas myVegas;
		Project proj = vegas.Project;
        foreach (Track track in proj.Tracks)
        {		
			
				if(track.Name == "Sync") MessageBox.Show("Audio a Sync detectado");
				foreach (TrackEvent trackEvent in track.Events)
				{
					if(trackEvent.MediaType == MediaType.Audio){
						
					}
				}
		}
		foreach (Track track in proj.Tracks)
        {
			foreach (TrackEvent trackEvent in track.Events)
			{					
				if(trackEvent.MediaType == MediaType.Video){
					
					VideoEvent vevnt = (VideoEvent)trackEvent;
					Envelopes vevntEnv = vevnt.Envelopes;
					
					if(vevntEnv.HasEnvelope(EnvelopeType.Velocity)){
						//Sensibilidad Detectada
						
						
						Envelope sensitivity = vevntEnv.FindByType(EnvelopeType.Velocity);
						sensitivity.Points.Clear();
						
						Timecode timeStart = trackEvent.Start; //Inicio del clip en milisegundos.
						
						
						Timecode timeFrame = new Timecode(1600);
						Timecode timeFrame2 = new Timecode(2600);
						Timecode timeFrame3 = new Timecode(3600);
						EnvelopePoint point_A = new EnvelopePoint(timeFrame,5);
						EnvelopePoint point_B = new EnvelopePoint(timeFrame2,2);
						EnvelopePoint point_C = new EnvelopePoint(timeFrame3,1);
						sensitivity.Points.Add(point_A);
						sensitivity.Points.Add(point_B);
						sensitivity.Points.Add(point_C);
						
						
						
						
					}
				}
            }
        }
	}
}