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
		
        /*foreach (Track track in proj.Tracks)
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
				
		}*/
		
		foreach (Track track in proj.Tracks)
        {
			
			foreach (TrackEvent trackEvent in track.Events)
			{					
				if(trackEvent.MediaType == MediaType.Video && trackEvent.Selected ){
					
					VideoEvent vevnt = (VideoEvent)trackEvent;
					Envelopes vevntEnv = vevnt.Envelopes;
					
					if(!vevntEnv.HasEnvelope(EnvelopeType.Velocity)) // Comprobamos si el video tiene envolvente
					{
						Envelope envelope = new Envelope(EnvelopeType.Velocity);
						vevntEnv.Add(envelope);
					}
					
					Envelope sensitivity = vevntEnv.FindByType(EnvelopeType.Velocity);
					sensitivity.Points.Clear();
					
					Timecode timeFirst = trackEvent.Start; //Inicio del clip en milisegundos.							
					Timecode timeLast = trackEvent.End;	//Fin del clip en milisegundos.		
					
					
					sensitivity.Points.GetPointAtX(Timecode.FromMilliseconds(0)).Y=3; //Modificamos el valor del punto inicial		
					
					foreach (Marker marker in proj.Markers) //Recorremos todos los marcadores	
					{
						if( marker.Position > timeFirst && marker.Position < timeLast && marker.Label=="" )  // Miramos si el marcador esta dentro del video
						{
							if(sensitivity.Points.GetPointAtX(marker.Position-timeFirst) == null){ // Comprobamos que no exista ya un punto en dicha posicion.
					
								EnvelopePoint point = new EnvelopePoint( marker.Position-timeFirst,0.15); //Creamos un nuevo punto en el marcador
								sensitivity.Points.Add(point);
				
							}
						}		
					}	
					
					EnvelopePoint pointEnd = new EnvelopePoint(timeLast,3); //Añadimos el punto final
					sensitivity.Points.Add(pointEnd);	
				}
            }
        }
	}
}
