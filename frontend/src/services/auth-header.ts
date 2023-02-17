export default function authHeader() {
  const userStr = localStorage.getItem("user");
  let user = null;
  if (userStr)
    user = JSON.parse(userStr);

  if (user && user.accessToken) {
    return { Authorization: 'Bearer ' + user.accessToken, 'Access-Control-Allow-Origin': '*' }; 
  } else {
    return { Authorization: '', 'Access-Control-Allow-Origin': '*'  };
  }
}