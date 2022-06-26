import authService from '../components/api-authorization/AuthorizeService';

export const getAccessToken = async () => {
    const token = await authService.getAccessToken();

    return token;
}