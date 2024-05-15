mergeInto(LibraryManager.library, {
    sendEventResponse: function (event) {
        console.log("sendEventResponse with message: " + UTF8ToString(event));

        if (typeof window !== "undefined") {
            const message = {
                type: UTF8ToString(event),
                event_id: UTF8ToString(event),
            };

            window.top.postMessage(message, "*");
        }
    },
});
