mergeInto(LibraryManager.library, {
    WebSocketConnect: function(url, userId, userAccessToken) {
        console.log("Executing WebSocketConnect");

    },
    WebSocketSend: function(message) {
        console.log("Executing WebSocketSend");
    },
    WebSocketClose: function() {
        console.log("Executing WebSocketClose");
    }
});
