mergeInto(LibraryManager.library, {
    SetupMessageEventListeners: function() {
        window.SetupMessageEventListeners = function() {  // Expose to global scope
            window.addEventListener('message', function(event) {
                if (event.data) {
                    var data = JSON.parse(event.data);
                    if (data.event_id === 'EL_GET_USER_CURRENCY') {
                        SendMessageToUnity('EventReceiver', 'HandleGetUserCurrency', event.data);
                    }
                    if (data.event_id === 'EL_USER_BALANCE') {
                        SendMessageToUnity('EventReceiver', 'HandleUserBalance', event.data);
                    }
                    if (data.event_id === 'EL_USER_INFORMATION') {
                        SendMessageToUnity('EventReceiver', 'HandleUserInformation', event.data);
                    }
                    if (data.event_id === 'EL_GET_EXPANDED_GAME_VIEW') {
                        SendMessageToUnity('EventReceiver', 'HandleExpandedGameView', event.data);
                    }
                }
            });
        };
    },
    SendMessageToUnity: function (gameObjectName, methodName, message) {
        window[gameObjectName][methodName](message);
    }
});
