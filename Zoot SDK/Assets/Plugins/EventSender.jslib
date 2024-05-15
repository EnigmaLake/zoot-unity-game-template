mergeInto(LibraryManager.library, {
    sendEventResponse: function (event) {
        console.log("test123: " + UTF8ToString(event));

        if (typeof window !== "undefined") {
            const message = {
                type: event,
                event_id: event,
            };

            window.top.postMessage(message, "*");
        }
    },
});
