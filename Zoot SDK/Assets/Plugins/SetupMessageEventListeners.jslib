mergeInto(LibraryManager.library, {
    SetupMessageEventListeners: function() {
        console.log("Setting up message event listeners");

        window.addEventListener('message', function(event) {
            console.log({ event });

            if (event.data) {
                if (event.data.event_id === 'EL_USER_BALANCE') {
                    SendMessage('WebSocket', 'HandleUserBalance', JSON.stringify(event.data));
                }
            }
       });
    }
});
