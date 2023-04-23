import axios from 'axios';
import dotenv from 'dotenv';

dotenv.config();

axios.defaults.baseURL = process.env.REACT_APP_API_URL;

const apiUrl = axios.defaults.baseURL;

axios.interceptors.response.use(function (response) {
  response.headers.test = 'special get headers';
  console.log("succes")
  return response;
}, function (error) {
  console.log("failed! the error is: ", error.message)
  return Promise.reject(error);
});

export default {
  getTasks: async () => {
    const result = await axios.get(`${apiUrl}/items`)
    return result.data;
  },

  addTask: async (name) => {
    console.log('addTask', name)
    const result = await axios.post(`${apiUrl}/items`, { id: 0, name: name, isComplete: false })
    return result.data;
  },

  setCompleted: async (id, isComplete) => {
    console.log('setCompleted', { id, isComplete })
    const result = await axios.put(`${apiUrl}/items/${id}`, { id: id, name: null, isComplete: isComplete })
    return result.data;
  },

  deleteTask: async (id) => {
    console.log('deleteTask')
    const result = await axios.delete(`${apiUrl}/items/${id}`)
    return result.data;
  }

};
