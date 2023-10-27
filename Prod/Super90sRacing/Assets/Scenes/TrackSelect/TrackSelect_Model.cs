using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TrackSelect_Model
{
    #region Serialized field
    private Text trackName = null;
    private Image trackImage = null;
    #endregion
    #region Variables
    private TrackDTO trackInfo = null;
    #endregion
    #region Constructor
    public TrackSelect_Model(Text trackName, Image trackImage)
    {
        this.trackName = trackName;
        this.trackImage = trackImage;
    }
    #endregion
    #region Init
    public void Init()
    {
        SetTrack(0);
    }
    #endregion
    #region Events
    public void SelectTrack(int id)
    {
        SetTrack(id);
    }
    #endregion
    #region Methods
    public void SetTrack(int id)
    {
        General.GetSetTrack = id;
        trackInfo = General.GetSetTracks.Where(x => x.id == id).First();                
        this.trackName.text = Language.GetLanguage[General.GetSetConfig.languageID][General.GetSetCountries.Where(x => x.id == trackInfo.countryId).First().idLanguage];
        trackImage.sprite = Resources.Load<Sprite>("TrackMap_" + id.ToString());
    }
    #endregion
}
