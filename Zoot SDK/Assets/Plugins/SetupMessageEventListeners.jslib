mergeInto(LibraryManager.library, {
    SetupMessageEventListeners: function() {
        console.log("Setting up message event listeners");

        window.addEventListener('message', function(event) {
            console.log({ event });

            if (event.data) {
                if (event.data.event_id === 'EL_GET_USER_CURRENCY') {
                    SendMessage('WebSocket', 'HandleGetUserCurrency', JSON.stringify(event.data));
                }

                if (event.data.event_id === 'EL_USER_BALANCE') {
                    SendMessage('WebSocket', 'HandleUserBalance', JSON.stringify(event.data));
                }

                if (event.data.event_id === 'EL_GET_EXPANDED_GAME_VIEW') {
                    SendMessage('WebSocket', 'HandleExpandedGameView', JSON.stringify(event.data));
                }

                if (event.data.event_id === 'EL_USER_INFORMATION') {
                    SendMessage('WebSocket', 'HandleUserInformation', JSON.stringify(event.data));
                }
            }
       });
    }
});
