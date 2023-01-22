import type { Values as LoginValues } from "components/LoginForm/types";
import type { Values as RegisterValues } from "components/RegisterForm/types";
import type { FormikHelpers } from "formik";
import { Box, Flex, Tab, TabList, TabPanel, TabPanels, Tabs } from "@chakra-ui/react";
import LoginForm from "components/LoginForm";
import RegistrationForm from "components/RegisterForm";
import useSelector from "hooks/useSelector";

const Auth = () => {
  const authStore = useSelector(store => store.authStore);

  const handleLogin = (values: LoginValues, action: FormikHelpers<LoginValues>) => {
    authStore.login(values)
      .finally(() => action.setSubmitting(false));
  };

  const handleRegister = (values: RegisterValues, action: FormikHelpers<RegisterValues>) => {
    authStore.register(values)
      .finally(() => action.setSubmitting(false));
  };

  return (
    <Flex h="100vh" justifyContent="center">
      <Box mt="10vh">
        <Tabs isFitted width="400px">
          <TabList>
            <Tab>Login</Tab>
            <Tab>Registration</Tab>
          </TabList>

          <TabPanels>
            <TabPanel>
              <LoginForm onSubmit={handleLogin} />
            </TabPanel>
            <TabPanel>
              <RegistrationForm onSubmit={handleRegister} />
            </TabPanel>
          </TabPanels>
        </Tabs>
      </Box>
    </Flex>
  )
};

export default Auth;