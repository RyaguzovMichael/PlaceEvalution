import axios from 'axios'

// Be careful when using SSR for cross-request state pollution
// due to creating a Singleton instance here;
// If any client changes this (global) instance, it might be a
// good idea to move this instance creation inside of the
// "export default () => {}" function below (which runs individually
// for each client)
const api = axios.create({
  baseURL: "http://localhost:5555",
  //timeout: 30000,
});

api.defaults.headers.common["Content-Type"] = "application/json";
api.defaults.withCredentials = true;

function errorHandler(error) {
  if (error.response?.status === 401) {
    localStorage.removeItem("ticket");
    window.location = "/";
  }
  return Promise.reject(error);
}

api.interceptors.request.use((config) => {
  return config;
}, errorHandler);

api.interceptors.response.use((response) => {
  return response;
}, errorHandler);

export { api }

// user
export const login = (loginCommand) => api.post("/User/Login", loginCommand);
export const logout = () => api.post("/User/Logout");
export const checkSessionAuthorization = () => api.get("/User/checkSessionAuthorization");
export const getAuthorizedCustomerData = () => api.get("/User/getAuthorizedCustomerData");
export const register = (registerCommand) => api.post("/User/Register", registerCommand);
export const changeLogin = (login) => api.post("/User/changeLogin", {login:login});
export const changePassword = (password, oldPassword) => api.post("/User/changePassword", {password:password, oldPassword:oldPassword});
export const changeUserData = (name, surname) => api.post("/User/changeUserData", {name:name, surname:surname});

//export const getShortModulesInfo = (courseId) => api.get(`/Modules/GetShortModulesInfoByCourseId`, {params:{courseId: courseId}});


