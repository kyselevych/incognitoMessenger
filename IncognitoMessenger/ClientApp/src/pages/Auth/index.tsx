import { Box, Flex, Tab, TabList, TabPanel, TabPanels, Tabs } from "@chakra-ui/react";
import logo from 'assets/logo.svg';
import LoginForm from "components/LoginForm";
import RegistrationForm from "components/RegistrationForm";

const Auth = () => {
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
              <LoginForm onSubmit={(values, action) => {}} />
            </TabPanel>
            <TabPanel>
              <RegistrationForm onSubmit={(values, action) => {}} />
            </TabPanel>
          </TabPanels>
        </Tabs>
      </Box>
    </Flex>
  )
};

export default Auth;