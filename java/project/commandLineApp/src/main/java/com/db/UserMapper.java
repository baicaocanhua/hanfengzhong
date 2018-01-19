package com.db;

import com.model.User;

/**
 * Created by maimai on 2018-01-19.
 */
public interface UserMapper {
    User getUser(Long id);

    int insertUser(User user);

    int deleteUser(Long id);
}
