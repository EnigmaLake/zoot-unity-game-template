mergeInto(LibraryManager.library, {
        socket: null, // Define a socket variable at the top level to keep the reference

        WebSocketConnect: function(urlUtf8, userIdUtf8, userAccessTokenUtf8) {
            const url = UTF8ToString(urlUtf8);
            const userId = UTF8ToString(userIdUtf8);
            const userAccessToken = UTF8ToString(userAccessTokenUtf8);
        
            console.log({ message: "Executing WebSocketConnect with parsed data", url, userId, userAccessToken });

            // Store the socket in the top level 'socket' variable
            this.socket = io(url, {
                auth: {
                    userId,
                    authorization: userAccessToken,
                },
                path: "/crash"
            });

            this.socket.on('connect', () => {
                console.log('Socket.IO connected');
            });

            // Recieving messages is handled separately by the PlatformEventReciever
            this.socket.on('message', (data) => {});

            this.socket.on('disconnect', () => {
                console.log('Socket.IO disconnected');
            });
        },

        WebSocketSend: function(message) {
            console.log("Executing WebSocketSend");
            if (this.socket) {
                this.socket.emit('message', message);
                console.log('Message sent:', message);
            } else {
                console.log('WebSocket is not connected.');
            }
        },

        WebSocketClose: function() {
            if (this.socket) {
                this.socket.close();
                console.log("WebSocket closed");
            } else {
                console.log('WebSocket is not connected.');
            }
        }
});
