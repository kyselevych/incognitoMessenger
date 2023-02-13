import AuthorizedRoute from "hoc/AuthorizedRoute";
import UnauthorizedRoute from "hoc/UnauthorizedRoute";
import Chat from "pages/Chat";
import Main from "pages/Main";
import { createBrowserRouter } from "react-router-dom";
import Auth from "./pages/Auth";

export enum Route {
  Index = '/',
  Auth = '/auth/',
  Chat = '/chat/'
};

export const router = createBrowserRouter([
  {
    path: "/",
    element: <AuthorizedRoute><Main /></AuthorizedRoute>,
    children: [
      {
        path: Route.Chat + ':chatId',
        element: <Chat />
      }
    ]
  },
  {
    path: "auth",
    element: <UnauthorizedRoute><Auth /></UnauthorizedRoute>
  },
]);