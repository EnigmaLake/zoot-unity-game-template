mergeInto(LibraryManager.library, {
  sendEventResponse: async function (event, data) {
      if (typeof window !== "undefined") {
        const message = JSON.parse(
          JSON.stringify({
            type: event,
            event_id: event,
            data,
          })
        );
        window.top?.postMessage(message, "*");
      }
  },
  apple: function() {
    console.log("Apple a-pear-s!");
  },
  getUserBalanceEvent: async function() {
    await this.sendEventResponse("EL_USER_BALANCE");
  },

  getUserCurrencyEvent: async function() {
    await this.sendEventResponse("EL_GET_USER_CURRENCY");
  },

  sendSetUserCurrencyEvent: async function(data) {
    await this.sendEventResponse("EL_SET_USER_CURRENCY", data);
  },

  sendSetGameRoundUuidEvent: async function(data) {
    await this.sendEventResponse("EL_SET_GAME_ROUND_UUID", data);
  },

  getUserInformationEvent: async function() {
    await this.sendEventResponse("EL_USER_INFORMATION");
  },

  loginUserEvent: async function() {
    await this.sendEventResponse("EL_LOGIN_USER");
  },

  purchaseCoinsEvent: async function() {
    await this.sendEventResponse("EL_PURCHASE_COINS");
  },

  showNotificationEvent: async function(message) {
    await this.sendEventResponse("EL_SHOW_TOAST", message);
  },

  toggleGameViewEvent: async function(data) {
    await this.sendEventResponse("EL_TOGGLE_EXPAND_GAME_VIEW", data);
  },

  getGameViewEvent: async function() {
    await this.sendEventResponse("EL_GET_EXPANDED_GAME_VIEW");
  },

  requestInitData: async function() {
    console.log("Called requestInitData");

    await this.sendEventResponse("EL_USER_BALANCE");
    await this.sendEventResponse("EL_GET_USER_CURRENCY");
    await this.sendEventResponse("EL_USER_INFORMATION");
    await this.sendEventResponse("EL_GET_EXPANDED_GAME_VIEW");
  },
});
