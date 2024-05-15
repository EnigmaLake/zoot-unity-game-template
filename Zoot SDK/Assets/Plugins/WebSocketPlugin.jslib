mergeInto(LibraryManager.library, {
    WebSocketConnect: function(url, userId, userAccessToken) {
            console.log("Executing WebSocketConnect");

            console.log({ url, userId, userAccessToken });

            console.log("Before socket initialisation in WebSocketConnect with ws");

            const socket = io("http://localhost:8080", {
                auth: {
                    userId: 15,
                    authorization: "no-token",
                },
                path: "/crash"
            });

            console.log("After socket initialisation in WebSocketConnect");

            socket.on('connect', () => {
                console.log('Socket.IO connected');
            });

            socket.on('message', (data) => {
                console.log('Received message:', data);
                // You can send data to Unity using SendMessage
                // SendMessage('GameObjectName', 'MethodName', data);
            });

            socket.on('disconnect', () => {
                console.log('Socket.IO disconnected');
            });


    },
    WebSocketSend: function(message) {
        console.log("Executing WebSocketSend");
    },
    WebSocketClose: function() {
        console.log("Executing WebSocketClose");
    }
});
