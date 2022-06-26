import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { getAccessToken } from './apiUtils';

const baseQueryFn = fetchBaseQuery({
  baseUrl: '/api/employees',
  prepareHeaders:  async (headers ) => {
      const accessToken = await getAccessToken();
      if (accessToken) {
          headers.set('authorization', `Bearer ${accessToken}`);
      }
      return headers;
  },
});

export const employeeApi = createApi({
  reducerPath: 'employeeApi',
  baseQuery: baseQueryFn,
  tagTypes: ['Employees'],
  endpoints: (builder) => ({
    getEmployees: builder.query({
      query: () => ({ method: 'GET'}),
      providesTags: (result = [], error, arg) => [
        'Employees',
        ...result.map(({ id }) => ({ type: 'Employees', id }))
      ]
    }),
    getEmployeeById: builder.query({
      query: (id) => ({ 
        url: `${id}`,
        method: 'GET'
      }),
      providesTags: (result, error, arg ) => [
        { type: 'Employees', id : arg.id }
      ]
    }),
    updateEmployee: builder.mutation({
      query: (employee) => ({
        method: 'PUT',
        body: employee
      }),
      invalidatesTags: (result, error, arg) => [{ type: 'Employees', id: arg.id }],
    }),
    saveEmployee: builder.mutation({
      query: (employee) => ({
        method: 'POST',
        body: employee
      }),
      invalidatesTags: ['Employees'],
    }),
    deleteEmployee: builder.mutation({
      query: (id) => ({
        url: `${id}`,
        method: 'DELETE'
      }),
      invalidatesTags: ['Employees'],
    }),
    calculateSalary: builder.mutation({
      query: (data) => ({
        url: '/calculate',
        method: 'POST',
        body: data
      })
    }),
  }),
})

export const { useGetEmployeesQuery, useGetEmployeeByIdQuery, useUpdateEmployeeMutation, useSaveEmployeeMutation, useDeleteEmployeeMutation, useCalculateSalaryMutation } = employeeApi
export const employeeApiHooks = {useGetEmployeesQuery, useGetEmployeeByIdQuery, useUpdateEmployeeMutation, useSaveEmployeeMutation, useDeleteEmployeeMutation, useCalculateSalaryMutation}