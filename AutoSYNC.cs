/**
 * AutoSYNC by lRaulMN7 Created 3/30/2019 
 **/

using System;
using System.Collections.Generic;
using System.Windows.Forms;

using ScriptPortal.Vegas; 

public class EntryPoint {
	
	public void FromVegas(Vegas vegas) {

		Project proj = vegas.Project;
		MarkerList allMarkers = proj.Markers;

        foreach (Track track in proj.Tracks)
        {		
				//Audio detection for later on...
				if(track.Name == "Sync"){
					//MessageBox.Show("Audio a Sync detectado");
					foreach (TrackEvent trackEvent in track.Events)
					{
						if(trackEvent.MediaType == MediaType.Audio){
							
						}
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
						
						Timecode timeFirst = trackEvent.Start; //Inicio del clip en milisegundos.				
						Timecode timeLast = trackEvent.End;			

						double jump = (timeLast.ToMilliseconds() - timeFirst.ToMilliseconds())/3;
						Timecode timeSecond = Timecode.FromMilliseconds(jump);
						Timecode timeThird = Timecode.FromMilliseconds(jump+jump);
					
						EnvelopePoint point_B = new EnvelopePoint(timeSecond,0.15);
						sensitivity.Points.Add(point_B);
						EnvelopePoint point_C = new EnvelopePoint(timeThird,0.15);
						sensitivity.Points.Add(point_C);
						EnvelopePoint point_D = new EnvelopePoint(timeLast,3);
						sensitivity.Points.Add(point_D);
				
					}
				}
            }
        }
	}
}

}