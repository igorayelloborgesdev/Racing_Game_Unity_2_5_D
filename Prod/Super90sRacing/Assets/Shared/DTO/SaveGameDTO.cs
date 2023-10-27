using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[System.Serializable]
public class SaveGameDTO
{
    #region Global
    public int id;
    public int idGameMode;
    public string dateTimeNow;
    #endregion
    #region Season
    public int trackIDSeason;
    public int teamIDSeason;
    public int playerIDSeason;
    public int difficultIDSeason;
    public List<DriverSeasonGeneral> driverSeasonGeneralList;
    #endregion
    #region Career
    public string name;
    public string code;
    public int countryId;
    public int avatarId;
    public ColorDTO helmetColor;
    public List<DriverDTO> careerDrivers;
    public int careerYear;
    public List<HistoricSeasonDTO> historicSeasonDTO;
    #endregion
    #region Challenge
    public bool isPlayerWonLastChallenge;
    public int challengerPoints;
    public int challengerPosition;
    public int challengerId;
    public int currentPoints;
    public int currentPosition;
    public int currentId;
    public bool isChallengeFinished;
    #endregion
}
