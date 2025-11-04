/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID PICKUP = 3978245845U;
        static const AkUniqueID PLAY_AMBIENCE_BLEND_CONTAINER___GREENHOUSE = 544979124U;
        static const AkUniqueID PLAY_AMBIENCE_BLEND_CONTAINER___INSIDE = 3981329107U;
        static const AkUniqueID PLAY_AMBIENCE_BLEND_CONTAINER_OUTSIDE = 1652549490U;
        static const AkUniqueID PLAY_AMBIENCE_GRAVEYARD = 3624085628U;
        static const AkUniqueID PLAY_BIRDCHIRPSEQUENCE = 1038362686U;
        static const AkUniqueID PLAY_DOOR_BASEMENT_BULKHEAD_CLOSE = 1751998966U;
        static const AkUniqueID PLAY_DOOR_BASEMENT_BULKHEAD_OPEN = 3610768546U;
        static const AkUniqueID PLAY_DOOR_HEAVY_CLOSE = 1205379775U;
        static const AkUniqueID PLAY_DOOR_HEAVY_OPEN = 79903961U;
        static const AkUniqueID PLAY_DOOR_LIGHT_CLOSE = 106752682U;
        static const AkUniqueID PLAY_DOOR_LIGHT_OPEN = 2289407054U;
        static const AkUniqueID PLAY_DOOR_MEDIUM_CLOSE = 2277063673U;
        static const AkUniqueID PLAY_DOOR_MEDIUM_OPEN = 3201265835U;
        static const AkUniqueID PLAY_FOOTSTEP = 1602358412U;
        static const AkUniqueID PLAY_FROGCROAKSEQUENCE = 3206454569U;
        static const AkUniqueID PLAY_GRAMOPHONE = 930871454U;
        static const AkUniqueID PLAY_GRANDFATHER_CLOCK_TICKING = 3529309445U;
        static const AkUniqueID PLAY_JUMP = 3689126666U;
        static const AkUniqueID PLAY_LANDING = 2323405115U;
        static const AkUniqueID PLAY_LIGHT_SWITCH = 1241030149U;
        static const AkUniqueID PLAY_MUSIC___INDOOR_SHRILL = 1553787382U;
        static const AkUniqueID PLAY_MUSIC_PLAYLIST_CONTAINER___GREENHOUSE = 1276784974U;
        static const AkUniqueID PLAY_MUSIC_PLAYLIST_CONTAINER___INSIDE = 3002568129U;
        static const AkUniqueID PLAY_MUSIC_PLAYLIST_CONTAINER___OUTSIDE = 3019702754U;
        static const AkUniqueID PLAY_OBJECTIVE_COMPLETED = 2316448637U;
        static const AkUniqueID PLAY_PHONE_RING = 3453892191U;
        static const AkUniqueID PLAY_PIANO = 1479974693U;
        static const AkUniqueID PLAY_PINK_NOISE = 3286096225U;
        static const AkUniqueID PLAY_SMALL_CLOCK = 813190480U;
        static const AkUniqueID PLAY_STINGER = 754369548U;
        static const AkUniqueID PLAY_TELEPHONE_DIAL_TONE = 3722310846U;
        static const AkUniqueID PLAY_THUNDER = 3870462868U;
        static const AkUniqueID PLAY_TOILET_FLUSH = 3094035634U;
        static const AkUniqueID PLAY_UI_DESELECT = 2036602390U;
        static const AkUniqueID PLAY_UI_SELECT = 3308548503U;
        static const AkUniqueID SET_SURFACE = 2231213135U;
        static const AkUniqueID STOP_GRAMOPHONE = 3959166920U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace AMBIENCE_STATE
        {
            static const AkUniqueID GROUP = 3875263435U;

            namespace STATE
            {
                static const AkUniqueID AMBIENCE_GREENHOUSE = 3831113035U;
                static const AkUniqueID AMBIENCE_INSIDE = 3695937480U;
                static const AkUniqueID AMBIENCE_OUTSIDE = 3996256281U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace AMBIENCE_STATE

        namespace GAME_STATE
        {
            static const AkUniqueID GROUP = 766723505U;

            namespace STATE
            {
                static const AkUniqueID MUSIC_GREENHOUSE = 2718585226U;
                static const AkUniqueID MUSIC_INSIDE = 3947247773U;
                static const AkUniqueID MUSIC_OUTSIDE = 4221858214U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace GAME_STATE

    } // namespace STATES

    namespace SWITCHES
    {
        namespace SURFACETYPE
        {
            static const AkUniqueID GROUP = 63790334U;

            namespace SWITCH
            {
                static const AkUniqueID CARPET = 2412606308U;
                static const AkUniqueID CONCRETE = 841620460U;
                static const AkUniqueID DIRT = 2195636714U;
                static const AkUniqueID GRASS = 4248645337U;
                static const AkUniqueID GRAVEL = 2185786256U;
                static const AkUniqueID TILE = 2637588553U;
                static const AkUniqueID WOOD = 2058049674U;
            } // namespace SWITCH
        } // namespace SURFACETYPE

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID BUS_VOLUME_AMBIENCE = 77985649U;
        static const AkUniqueID BUS_VOLUME_FOOTSTEPS = 4073739390U;
        static const AkUniqueID BUS_VOLUME_MUSIC = 2849708270U;
        static const AkUniqueID BUS_VOLUME_SFX = 194530214U;
        static const AkUniqueID RTPC_OCCLUSION_LOPASS = 222722569U;
        static const AkUniqueID RTPC_OCCLUSION_LOPASS_KITCHEN_CLOCK = 1543641979U;
        static const AkUniqueID RTPC_OCCLUSION_VOLUME = 342905355U;
        static const AkUniqueID RTPC_OCCLUSION_VOLUME_KITCHEN_CLOCK = 4273873553U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID AMBIENCE_BUS = 4202237879U;
        static const AkUniqueID FOOTSTEPS_BUS = 2247540952U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSIC_BUS = 3127962312U;
        static const AkUniqueID SFX_BUS = 1502772432U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
