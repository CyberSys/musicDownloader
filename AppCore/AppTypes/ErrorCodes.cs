using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.AppTypes
{
    internal enum ErrorCodes
    {
        //General 
        GeneralException = 0xFF,
        InfoMessage = 0x1F,

        //RequestMonitor dispose
        RequestMonitorDispose = 0x01,
        //Credentials check
        CheckCredentialsCompleted = 0x02,
        CheckCredentialsParse = 0x03,
        CheckCredentialsRequest = 0x0E,
        //Load tracks list
        LoadTrackListCompleted = 0x04,
        LoadTrackListRequest = 0x05,
        LoadTracksListParse = 0x06,

        //DownloadStack dispose
        DownloadsStackDispose = 0x07,
        //Tracks download
        TrackDownloadRequest = 0x08,
        TrackDownloadReadWrite = 0x09,
        TrackDownloadCompleted = 0x0A,
        
        //Friends
        LoadFriendsCompleted = 0x0B,
        LoadFriendsRequest = 0x0C,
        LoadFriendsParse = 0x0D
    }
}
