import { createBrowserRouter } from "react-router-dom";
import Auth from "./pages/Auth";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <h1>Main</h1>
  },
  {
    path: "auth",
    element: <Auth />
  },
]);