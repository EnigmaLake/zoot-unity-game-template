mergeInto(LibraryManager.library, {
    socket: null, // Define a socket variable at the top level to keep the reference

    GameServerSocketConnect: function(urlUtf8, userIdUtf8, userAccessTokenUtf8) {
        var url = UTF8ToString(urlUtf8);
        var userId = UTF8ToString(userIdUtf8);
        var userAccessToken = UTF8ToString(userAccessTokenUtf8);

        console.log({ message: "Executing WebSocketConnect with parsed data", url, userId, userAccessToken });

        // Initialize the socket connection
        this.socket = io(url, {
            auth: {
                userId: userId,
                authorization: userAccessToken,
            },
            path: "/crash"
        });

        this.socket.on('connect', function() {
            console.log('Socket.IO connected');
        });

        this.socket.on('disconnect', function() {
            console.log('Socket.IO disconnected');
        });
    },

    GameServerSocketOn: function(eventNameUtf8, callback) {
        var eventName = UTF8ToString(eventNameUtf8);

        console.log("Executing GameServerSocketOn for event: " + eventName);

        if (this.socket) {
            this.socket.on(eventName, function(data) {
                SendMessage('GameServerSocketManager', 'HandleSocketOnMessage', JSON.stringify(data));
            });
        } else {
            console.log('WebSocket is not connected. Cannot add event listener.');
        }
    },

    GameServerSocketSend: function(eventNameUtf8, payloadUtf8) {
        var eventName = UTF8ToString(eventNameUtf8);
        var payload = JSON.parse(UTF8ToString(payloadUtf8));

        console.log("Executing GameServerSocketSend");

        if (this.socket) {
            this.socket.emit(eventName, payload);
            console.log(`Event '${eventName}' sent with payload:`, payload);
        } else {
            console.log('WebSocket is not connected.');
        }
    },

    GameServerSocketClose: function() {
        if (this.socket) {
            this.socket.close();
            console.log("WebSocket closed");
        } else {
            console.log('WebSocket is not connected.');
        }
    }
});
