const EL_USER_BALANCE = "EL_USER_BALANCE";
const EL_GET_USER_CURRENCY = "EL_GET_USER_CURRENCY";
const EL_SET_USER_CURRENCY = "EL_SET_USER_CURRENCY";
const EL_SET_GAME_ROUND_UUID = "EL_SET_GAME_ROUND_UUID";
const EL_USER_INFORMATION = "EL_USER_INFORMATION";
const EL_LOGIN_USER = "EL_LOGIN_USER";
const EL_PURCHASE_COINS = "EL_PURCHASE_COINS";
const EL_SHOW_TOAST = "EL_SHOW_TOAST";
const EL_TOGGLE_EXPAND_GAME_VIEW = "EL_TOGGLE_EXPAND_GAME_VIEW";
const EL_GET_EXPANDED_GAME_VIEW = "EL_GET_EXPANDED_GAME_VIEW";

const sendEventResponse = async (
  event,
  data,
) => {
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
};


mergeInto(LibraryManager.library, {
  apple: function() {
    console.log("Apple a-pear-s!");
  },
  getUserBalanceEvent: async function() {
    await sendEventResponse(EL_USER_BALANCE);
  },

  getUserCurrencyEvent: async function() {
    await sendEventResponse(EL_GET_USER_CURRENCY);
  },

  sendSetUserCurrencyEvent: async function(data) {
    await sendEventResponse(EL_SET_USER_CURRENCY, data);
  },

  sendSetGameRoundUuidEvent: async function(data) {
    await sendEventResponse(EL_SET_GAME_ROUND_UUID, data);
  },

  getUserInformationEvent: async function() {
    await sendEventResponse(EL_USER_INFORMATION);
  },

  loginUserEvent: async function() {
    await sendEventResponse(EL_LOGIN_USER);
  },

  purchaseCoinsEvent: async function() {
    await sendEventResponse(EL_PURCHASE_COINS);
  },

  showNotificationEvent: async function(message) {
    await sendEventResponse(EL_SHOW_TOAST, message);
  },

  toggleGameViewEvent: async function(data) {
    await sendEventResponse(EL_TOGGLE_EXPAND_GAME_VIEW, data);
  },

  getGameViewEvent: async function() {
    await sendEventResponse(EL_GET_EXPANDED_GAME_VIEW);
  },

  requestInitData: async function() {
    await getUserBalanceEvent();
    await getUserCurrencyEvent();
    await getUserInformationEvent();
    await getGameViewEvent();
  },
});
