
import { Navigate, Outlet } from "react-router-dom";
import { getAccessToken } from "../services/authService";

function ProtectedRoute() {
  const token = getAccessToken();

  if (!token) {
    return (
      <Navigate
        to="/login"
        replace
        state={{ motivo: "sinSesion" }}
      />
    );
  }

  return <Outlet />;
}

export default ProtectedRoute;
