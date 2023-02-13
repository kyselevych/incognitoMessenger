import { extendTheme, ThemeConfig  } from '@chakra-ui/react'
import { mode } from "@chakra-ui/theme-tools";

const config: ThemeConfig = {
  initialColorMode: 'dark',
  useSystemColorMode: false,
};

const colors = {
  theme: {
    100: '#697e8d',
    200: '#4b4b4b',
    600: '#697e8d',
    700: '#30323f',
    800: '#1f2029',
    900: '#17181f'
  },
  accent: {
    100: '#fdf5d3',
    200: '#888888',
    300: '#4b4b4b'
  }
};

const styles = {
  global: (props: any) => ({
    body: {
      bg: mode('#fff', colors.theme[900])(props),
      color: mode('#000', 'f0f1ff')(props)
    }
  })
};

const fonts = {
  body: `'Poppins', sans-serif`
};

const theme = extendTheme({ config, colors, styles, fonts })

export default theme;