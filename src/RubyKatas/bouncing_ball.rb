def bouncing_ball(h, bounce, window)
  if h <=0 or bounce >= 1 or bounce <= 0 or window >= h
    print (-1)
    return -1
  end
  tempheight = h * bounce
  count = 1
  while (tempheight > window)
    count += 2
    tempheight *= bounce
  end
  print count
  return count
end

bouncing_ball(3, 0.66, 1.5) # should return 3
bouncing_ball(30, 0.66, 1.5) # should return 15
bouncing_ball(30, 0.75, 1.5) # should return 21
bouncing_ball(30, 0.4, 10) # should return 3
bouncing_ball(40, 1, 10) # should return -1
bouncing_ball(10, -1, 10) # should return -1
bouncing_ball(10, 0.5, 10) # should return -1
bouncing_ball(-5, 0.66, 1.5) # should return -1